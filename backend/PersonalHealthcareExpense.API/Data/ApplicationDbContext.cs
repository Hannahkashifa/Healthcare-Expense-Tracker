using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Expense> Expenses { get; set; } = null!;

        public DbSet<Income> Incomes { get; set; } = null!;

        public DbSet<Healthcare> Healthcares { get; set; } = null!;
        
        public DbSet<Medicine> Medicines { get; set; } = null!;
    }
}