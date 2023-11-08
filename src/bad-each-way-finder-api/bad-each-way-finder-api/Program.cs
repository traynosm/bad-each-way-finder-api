using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api.Configuration;
using bad_each_way_finder_api.Controllers;
using bad_each_way_finder_api_auth.Settings;
using bad_each_way_finder_api_exchange.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace bad_each_way_finder_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathToContentRoot = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(pathToContentRoot)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(Program).Assembly)
                .AddCommandLine(args)
                .Build();

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
            builder.Services.ConfigureExchange();

            builder.Services.Configure<ExchangeSettings>(o => 
            builder.Configuration.GetSection("ExchangeSettings")
                .Bind(o));

            builder.Services.Configure<LoginSettings>(o =>
            builder.Configuration.GetSection("LoginSettings")
                .Bind(o));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            //app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            app.Run();
        }
    }
}