using ManagementNote.Model;

namespace ManagementNote.Services
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetByUserName(CreateUser user);
        User Create(CreateUser user);
        Boolean UpdateAvata(string url);
        void Delete(int id);
    }
}
