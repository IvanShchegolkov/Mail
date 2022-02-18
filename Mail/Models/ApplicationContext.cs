using Microsoft.EntityFrameworkCore;

namespace Mail.Models
{
    /// <summary>
    /// Контекст данных EF
    /// Созадна миграция
    /// </summary>
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
