using Microsoft.EntityFrameworkCore;
using ExpenseEZ.Models;

namespace ExpenseEZ.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ExpenseEZ.Models.User> Users { get; set; }
		public DbSet<ExpenseEZ.Models.Department> Departments { get; set; }
		public DbSet<ExpenseEZ.Models.Category> Categories { get; set; }
		public DbSet<ExpenseEZ.Models.Budget> Budgets { get; set; }
		public DbSet<ExpenseEZ.Models.AuditLog> AuditLogs { get; set; }
	}
}