using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bad_each_way_finder_api.Areas.Identity.Data;

namespace bad_each_way_finder_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration
                .GetConnectionString("BadEachWayFinderApi") ?? 
                throw new InvalidOperationException("Connection string 'BadEachWayFinderApi' not found.");

            builder.Services.AddDbContext<BadEachWayFinderApiContext>(options => 
            options.UseSqlite(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BadEachWayFinderApiContext>();

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}