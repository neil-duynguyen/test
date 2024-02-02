using jdk.nashorn.@internal.ir;
using ManagementNote.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ManagementNote.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        public static User UserLogin;
        public AccountRepository(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
        }

        public async Task<TokenModel> Login(string username, string password)
        {
            var _user = _context.userDb.FirstOrDefault<User>(u => u.UserName.ToLower().Equals(username) && u.Password.Equals(password));
            if (_user == null)
            {
                return null;
            }
            _user.Lastlogin = DateTime.Now;
            UserLogin= _user;
            await _context.SaveChangesAsync();
            return await GenerateToken(_user);
        }

        //Method cấp token
        private async Task<TokenModel> GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //lưu database
            var refreshTokenEntity = new RefreshToken
            { 
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                UserNameToken = user.UserName,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IsSueAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(3)
            };

            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken, 
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            { 
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<TokenModel> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretKey"]);
            var tokenValidateParam = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                //tự cấp token 
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, // không kiểm tra token đã hết hạn hay chưa
            };
            try
            {
                //check 1: AccessToken valid format 
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken,
                    tokenValidateParam, out var validatedToken);


                //check 2: check thuật toán genera ra token
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals
                        (SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)//false
                    {
                        throw new Exception("Invalid token");
                    }
                }
                //check 3: check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x =>
                    x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    throw new Exception("Access token has not yet expired");
                }

                //check 4 : check refreshToken exist in DB
                var storedToken = _context.refreshToken.FirstOrDefault(x => x.Token ==
                    model.RefreshToken);
                if (storedToken == null)
                {
                    throw new Exception("Refresh token does not exit");
                }

                //check 5: check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                    throw new Exception("Refresh token as been used");

                if (storedToken.IsRevoked)
                    throw new Exception("Refresh token has been revoked");

                //check 6: check accessToken id == jwtID in refreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type ==
                    JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                    throw new Exception("Token doesn't match");

                //update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //create new token
                var user = await _context.userDb.SingleOrDefaultAsync(x => x.UserName ==
                    storedToken.UserNameToken);
                return await GenerateToken(user);
            }
            catch (SecurityTokenValidationException stvex)
            {
                throw new Exception("Token validation failed", stvex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
}
