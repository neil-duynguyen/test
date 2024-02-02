using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementNote.Model
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public string UserNameToken { get; set; }
        [ForeignKey(nameof(UserNameToken))]
        public User User { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IsSueAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
