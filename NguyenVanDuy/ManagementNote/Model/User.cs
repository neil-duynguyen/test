using System.Collections.Generic;

namespace ManagementNote.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; } = null;
        public string Role { get; set; }
        public Boolean Status { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public DateTime Lastlogin { get; set; }
        public string? Avata { get; set; } = null;

        public ICollection<Note> Notes { get; set;}

        public User() {
            Notes= new List<Note>();
        }
    }
}
