using Microsoft.EntityFrameworkCore;
using StudentDiaryServer.Model.Entity;

namespace StudentDiaryServer.Model
{
    public class AppDbContext : DbContext
    {
        //таблица аккаунтов
        public DbSet<Account> Accounts { get; set; }
        //таблица типов аккаунта
        public DbSet<AccountType> AccountTypes { get; set; }
        //Таблица с логинами-паролями
        public DbSet<LoginPassword> LoginPasswords { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
                AddJsonFile("appsettings.json").
                Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("RemoteConnection"));
        }
    }
}
