using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KStore.Data.EntityFramework
{
    public class KStoreDbContextFactory : IDesignTimeDbContextFactory<KStoreDBContext>
    {
        public KStoreDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("KstoreDb");
            var optionsBuilder = new DbContextOptionsBuilder<KStoreDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new KStoreDBContext(optionsBuilder.Options);
        }

        
    }
}
