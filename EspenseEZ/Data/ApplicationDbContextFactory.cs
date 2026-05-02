using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EspenseEZ.Data
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

			optionsBuilder.UseNpgsql("Host=switchyard.proxy.rlwy.net;Port=54596;Database=railway;Username=postgres;Password=TqsoFmYkOmeVcRgSulLzvkEYVeUwNtMb;SSL Mode=Require;Trust Server Certificate=true");

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}