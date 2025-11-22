/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Restore.Infrastructure.Database;


namespace Restore.Infrastructure.Persistence.DbContexts;

 
public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var connectionString = DbConnectionHelper.GetConnectionStringForContext<StoreDbContext>();

        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();

        return new StoreDbContext(optionsBuilder.Options);
    }
} */
