/* using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Restore.Infrastructure.Database;

public static class DbConnectionHelper
{
    private static IConfigurationRoot? _configuration;

    /// <summary>
    /// Loads and caches configuration from appsettings.json and environment variables.
    /// </summary>
    private static IConfigurationRoot Configuration
    {
        get
        {
            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            }

            return _configuration;
        }
    }

    /// <summary>
    /// Get connection string by name (e.g., DefaultConnection, AuthConnection, OrgConnection)
    /// </summary>
    public static string GetConnectionString(string name = "DefaultConnection")
    {
        var connectionString = Configuration.GetConnectionString(name);

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException($"Connection string '{name}' not found in configuration.");

        return connectionString;
    }

    /// <summary>
    /// Get connection string dynamically by DbContext type
    /// (Convention-based: looks for connection name = context name without "DbContext" suffix)
    /// </summary>
    public static string GetConnectionStringForContext<T>() where T : DbContext
    {
        var contextName = typeof(T).Name.Replace("DbContext", "");
        var connectionString = Configuration.GetConnectionString(contextName);

        // fallback to DefaultConnection if not found
        return !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : GetConnectionString("DefaultConnection");
    }
} */


/* 
##### appsettings.json structure ##### 
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=IMS;Username=postgres;Password=postgres",
    "Auth": "Host=localhost;Database=AuthDb;Username=postgres;Password=postgres",
    "Org": "Host=localhost;Database=OrgDb;Username=postgres;Password=postgres"
  }
}

With this structure:

DbConnectionHelper.GetConnectionStringForContext<AuthDbContext>() → uses Auth
DbConnectionHelper.GetConnectionStringForContext<OrgDbContext>() → uses Org
Falls back to DefaultConnection if none found.

 */

/* 
    ###### Nuget package:  

    Microsoft.Extensions.Configuration.EnvironmentVariables -> allows .AddEnvironmentVariables()

    dotnet add package Microsoft.Extensions.Configuration → core configuration types
    dotnet add package Microsoft.Extensions.Configuration.FileExtensions  → allows .AddJsonFile()
    dotnet add package Microsoft.Extensions.Configuration.Json → parses appsettings.json
 */
/* 

/* var config = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();

        var connectionString = config.GetConnectionString("DefaultConnection"); 

        // Read the connection string from environment or fallback
        //var connectionString = "Host=localhost;Database=AuthDb;Username=postgres;Password=yourpassword";
 */