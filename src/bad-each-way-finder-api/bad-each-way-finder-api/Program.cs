using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BadEachWayFinderApiContext>();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureAuth();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}