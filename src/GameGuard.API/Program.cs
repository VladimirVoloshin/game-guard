using Microsoft.AspNetCore.Cors.Infrastructure;

internal class Program
{
    private const string CORS_ORIGINS_SET = "CorsOrigins";
    private const string CORS_POLICY = "CorsPolicy";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var config = BuildConfiguration(builder);
        ConfigureLocalCORSSettings(builder, config);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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
}
