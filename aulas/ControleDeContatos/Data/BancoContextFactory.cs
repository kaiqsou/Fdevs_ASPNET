using ControleDeContatos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ControleDeContatos
{
    public class BancoContextFactory : IDesignTimeDbContextFactory<BancoContext>
    {
        public BancoContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BancoContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DataBase"));

            return new BancoContext(optionsBuilder.Options);
        }
    }
}
