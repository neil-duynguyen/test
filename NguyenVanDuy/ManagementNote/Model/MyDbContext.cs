using Microsoft.EntityFrameworkCore;

namespace ManagementNote.Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> userDb { get; set; }
        public DbSet<Note> noteDb { get; set; }
        public DbSet<RefreshToken> refreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(u => u.UserName);
                e.Property(u => u.DateCreated).HasDefaultValue(DateTime.Now);
                e.Property(u => u.Lastlogin).HasDefaultValue(DateTime.Now);
            });

            modelBuilder.Entity<Note>(e => {
                e.ToTable("Note");
                e.Property(u => u.DateCreate).HasDefaultValue(DateTime.Now);
                e.Property(u => u.DateUpdate).HasDefaultValue(DateTime.Now);
                e.HasOne(u => u.user)
                .WithMany(u => u.Notes)
                .HasForeignKey(u => u.UserName);
            });
        }
    }
}
