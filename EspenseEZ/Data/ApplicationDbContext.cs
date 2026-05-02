using Microsoft.EntityFrameworkCore;

namespace EspenseEZ.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<TestUser> TestUsers { get; set; }
	}

	public class TestUser
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
	}
}