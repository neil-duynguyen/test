using ManagementNote.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ManagementNote.Services
{

    public class AdminRepository : IAdminRepository
    {
        private readonly MyDbContext _context;

        public AdminRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<ViewUser> GetAllUser()
        {
            List<ViewUser> viewUsers= new List<ViewUser>();
            var user = _context.userDb.ToList();
            foreach (var i in user)
            {
                var NNote = _context.noteDb.ToList().Count(v => v.UserName.Equals(i.UserName));//.Where(v => v.UserName.Equals(i.UserName)).GroupBy(v => v.UserName)
                viewUsers.Add(new ViewUser(i.UserName, NNote, i.Lastlogin, i.DateCreated, i.Avata));
            }
            return viewUsers;
        }

        public bool Lock(string userName, bool a)
        {
            var user = _context.userDb.SingleOrDefault(v => v.UserName.Equals(userName));
            if (user != null) { 
                user.Status = a;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(string userN, string role)
        {
            var user = _context.userDb.SingleOrDefault(v => v.UserName.Equals(userN));
            if (user != null && role == "Admin")
            {
                user.Role = role;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
