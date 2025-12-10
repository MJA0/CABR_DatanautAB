using DatanautAB.Data;

namespace DatanautAB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            services.AddDbContext<DatanautContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatanautDB")));
        }
    }
}
