using Microsoft.EntityFrameworkCore;

namespace Mail.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Mail> Mail { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
