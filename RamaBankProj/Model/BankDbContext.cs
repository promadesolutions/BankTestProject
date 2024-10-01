using Microsoft.EntityFrameworkCore;

namespace RamaBankProj.Model
{
    public class BankDbContext(DbContextOptions<BankDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DirectDebit> DirectDebits { get; set; }
    }
}