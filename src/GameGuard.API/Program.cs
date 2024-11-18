using GameGuard.Application.ActivityLogs;
using GameGuard.Application.Players;
using GameGuard.Domain.ActivityLogs;
using GameGuard.Domain.ActivityLogs.Events;
using GameGuard.Domain.ActivityLogs.Specifications;
using GameGuard.Domain.Players;
using GameGuard.Infrastructure;
using GameGuard.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private const string CORS_ORIGINS_SET = "CorsOrigins";
    private const string CORS_POLICY = "CorsPolicy";
    private const string DB_CONNECTION_STRING_SET = "DefaultConnection";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        RegisterServices(builder);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var config = BuildConfiguration(builder);
        ConfigureLocalCORSSettings(builder, config);

        ConfigureDatabase(builder);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(CORS_POLICY);

        app.Run();
    }

    private static void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
        builder.Services.AddScoped<IPlayerService, PlayerService>();
        builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
        builder.Services.AddScoped<IActivityLogService, ActivityLogService>();
        builder.Services.AddScoped<INotificationHandler<ActivityCreatedEvent>, ActivityMonitor>();
        builder.Services.AddScoped<SuspiciousPlayerCompositeSpec>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    }

    private static IConfigurationRoot BuildConfiguration(WebApplicationBuilder builder)
    {
        return builder
            .Configuration.AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();
    }

    private static void ConfigureLocalCORSSettings(
        WebApplicationBuilder builder,
        IConfigurationRoot config
    )
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(opt =>
            {
                var corsOrigin = config.GetSection(CORS_ORIGINS_SET).Value ?? "";
                opt.AddPolicy(
                    CORS_POLICY,
                    policy =>
                    {
                        policy
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders("WWW-Authenticate")
                            .WithOrigins(corsOrigin);
                    }
                );
            });
        }
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString(DB_CONNECTION_STRING_SET),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            )
        );

        if (builder.Environment.IsDevelopment())
        {
            var serviceProvider = builder.Services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            ClearActivityLogs(dbContext);
        }
    }

    private static void ClearActivityLogs(AppDbContext dbContext)
    {
        dbContext.ActivityLogs.RemoveRange(dbContext.ActivityLogs);

        dbContext.SaveChanges();
    }
}
