using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api.Configuration;
using bad_each_way_finder_api.Controllers;
using bad_each_way_finder_api.Repository;
using bad_each_way_finder_api_auth.Settings;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_exchange.Settings;
using bad_each_way_finder_api_sportsbook.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            var connectionString = configuration
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
            builder.Services.ConfigureSportsbook();

            builder.Services.AddScoped<IExchangeDatabaseService, ExchangeDatabaseService>();
            builder.Services.AddScoped<ISportsbookDatabaseService, SportsbookDatabaseService>();

            builder.Services.Configure<ExchangeSettings>(o => 
            configuration.GetSection("ExchangeSettings")
                .Bind(o));

            builder.Services.Configure<SportsbookSettings>(o =>
            configuration.GetSection("SportsbookSettings")
                .Bind(o));

            builder.Services.Configure<LoginSettings>(o =>
            configuration.GetSection("LoginSettings")
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