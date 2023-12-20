using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api.Configuration;
using bad_each_way_finder_api.Repository;
using bad_each_way_finder_api.Services;
using bad_each_way_finder_api.Settings;
using bad_each_way_finder_api.Workers;
using bad_each_way_finder_api_auth.Settings;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_exchange.Settings;
using bad_each_way_finder_api_sportsbook.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

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

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["IdentitySettings:ValidIssuer"],
                    ValidAudience = builder.Configuration["IdentitySettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration["IdentitySettings:Secret"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.HandleResponse();
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token validation failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication JWT",
                    Version = "v1"
                });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services.ConfigureAuth();
            builder.Services.ConfigureExchange();
            builder.Services.ConfigureSportsbook();

            builder.Services.AddScoped<IRaceService, RaceService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddScoped<IExchangeDatabaseService, ExchangeDatabaseService>();
            builder.Services.AddScoped<ISportsbookDatabaseService, SportsbookDatabaseService>();
            builder.Services.AddScoped<IAccountDatabaseService, AccountDatabaseService>();
            builder.Services.AddScoped<IPropositionDatabaseService, PropositionDatabaseService>();
            builder.Services.AddScoped<IRaceDatabaseService, RaceDatabaseService>();

            builder.Services.AddScoped<IScopedRacingWorker, ScopedRacingWorker>();
            builder.Services.AddHostedService<RacingWorker>();

            builder.Services.Configure<ExchangeSettings>(o => 
            configuration.GetSection("ExchangeSettings")
                .Bind(o));

            builder.Services.Configure<SportsbookSettings>(o =>
            configuration.GetSection("SportsbookSettings")
                .Bind(o));

            builder.Services.Configure<LoginSettings>(o =>
            configuration.GetSection("LoginSettings")
                .Bind(o));

            builder.Services.Configure<IdentitySettings>(o =>
            configuration.GetSection("IdentitySettings")
                .Bind(o));

            builder.Services.Configure<RaceWorkerSettings>(o =>
            configuration.GetSection("RaceWorkerSettings")
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