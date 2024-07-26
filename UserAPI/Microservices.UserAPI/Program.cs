using Microservices.UserAPI;
using Microservices.UserAPI.Data;
using Microservices.UserAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    
    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();


    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().Wait();

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser
            {
                UserName = "cagri.durmus",
                Email = "cagridurmus@hotmail.com",
            };
            userManager.CreateAsync(user, "Asd123**").Wait();
        }
    }


    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
