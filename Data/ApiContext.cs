using BankSystemApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystemApp.Data
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "UserAccountDb");
        }
        public DbSet<UserAccountModel> UserAccount { get; set; }
    }
}
