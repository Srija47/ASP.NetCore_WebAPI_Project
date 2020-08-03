using Microsoft.EntityFrameworkCore;

namespace UserModule.Models
{
    public partial class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         { 
         optionsBuilder.UseSqlServer(@"Server=WIN8\SQLEXPRESS;Database=UserDB;Trusted_Connection=True;");
        }
        public DbSet<User> Users { get; set; }
    
    }
}
