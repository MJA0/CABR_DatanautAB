using DatanautAB.Data;
using DatanautAB.UI.MainMenu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;
using DatanautAB.DataSeed;
using DatanautAB.Models;
using DatanautAB.UI.MainMenu;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DatanautAB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //services.AddDbContext<DatanautContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DatanautDB")));

            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var appRoot = Directory.GetParent(exePath).Parent.Parent.FullName;
            Console.WriteLine(appRoot);

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appRoot)  // AppContext.BaseDirectory points to the runtime folder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Setup DI container
            var services = new ServiceCollection();

            services.AddDbContext<DatanautContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationProgramDB")));

            var provider = services.BuildServiceProvider();

            // Resolve DbContext
            using var db = provider.GetRequiredService<DatanautContext>();

            MainMenuUI.Show(db);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appRoot)  // AppContext.BaseDirectory points to the runtime folder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Setup DI container
            var services = new ServiceCollection();

            services.AddDbContext<DatanautContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatanautDB")));

            var provider = services.BuildServiceProvider();

            // Resolve DbContext
            using var db = provider.GetRequiredService<DatanautContext>();

            MainMenuUI.Show(db);
        }
    }
}
