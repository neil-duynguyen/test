using ManagementNote.Model;

namespace ManagementNote.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }
        public User Create(CreateUser CUser)
        {
            var _user = new User
            {
                UserName = CUser.UserName,
                Password = CUser.Password,
                Role = "user",
            };
            _context.Add(_user);
            _context.SaveChanges();
            return new User
            {
                UserName = _user.UserName,
                Password = _user.Password,
                Email = _user.Email,
                Role = _user.Role,
                Status = _user.Status,
                DateCreated = DateTime.Now,
                Avata = _user.Avata
            };
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            var users = _context.userDb.ToList<User>();
            return users;
        }

        public User GetByUserName(CreateUser user)
        {
            var _user = _context.userDb.FirstOrDefault<User>(u => u.UserName.ToLower().Equals(user.UserName));
            return _user;
        }

        public bool UpdateAvata(string url)
        {
            var UserN = ManagementNote.Services.AccountRepository.UserLogin.UserName;
            var user = _context.userDb.SingleOrDefault(x => x.UserName.Equals(UserN));
            if (user != null) {
                user.Avata = url;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
