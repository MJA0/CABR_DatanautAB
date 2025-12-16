using DatanautAB.Data;
using DatanautAB.DataSeed;
using DatanautAB.Models;
using DatanautAB.UI.MainMenu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatanautAB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<DatanautContext>()
                .UseSqlServer(configuration.GetConnectionString("DatanautDB"))
                .Options;

            using var context = new DatanautContext(options);

            // Seed körs EN gång
            DbSeeder.Seed(context);

            MainMenuUI.Show(context);

        }
    }
}
