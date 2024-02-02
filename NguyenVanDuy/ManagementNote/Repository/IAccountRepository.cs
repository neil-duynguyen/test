using ManagementNote.Model;

namespace ManagementNote.Services
{
    public interface IAccountRepository
    {
        Task<TokenModel> Login(string username, string password);
        //void Logout();
        Task<TokenModel> RenewToken(TokenModel tokenModel);
    }
}
