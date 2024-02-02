using ManagementNote.Model;
using ManagementNote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ManagementNote.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var _user = await _accountRepository.Login(userName, password);
            if (_user == null)
            {
                return NotFound("UserName or Password is incorrect.");
            }
            return Ok(new { 
                Success = true,
                Message = "Authenticate success",
                Data = _user
            });
        }

        [HttpPost]
        public async Task<IActionResult> RenewToken(TokenModel tokenModel)
        {
            try
            {
                var refreshToken = await _accountRepository.RenewToken(tokenModel);
                return Ok(new {
                    Success = true,
                    Message = "Authenticate success",
                    Data = refreshToken
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
