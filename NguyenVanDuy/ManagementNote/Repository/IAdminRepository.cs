using ManagementNote.Model;

namespace ManagementNote.Services
{
    public interface IAdminRepository
    {
        List<ViewUser> GetAllUser();
    //    User GetById(int id);
        Boolean Update(string user, string role);
        Boolean Lock(string userName, Boolean a);
    }
}
