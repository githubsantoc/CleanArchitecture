using Application;
using Application.Wrapper;
using CQRSApplication;
using Hangfire;
using Infrastructure;
using Infrastructure.WrapperImp;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});



builder.Services
    .AddAppliServices(builder.Configuration)
    .AddInfraServices(builder.Configuration)
    .AddPresentation();


//for serilog
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

//in order to implement serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
           .WriteTo.Console()
           .WriteTo.File("logs/log2-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<string>>>();
    var roles = new[] { "Admin", "Customer" };
    foreach (var role in roles)
    {
        if (!await rolesManager.RoleExistsAsync(role))
            await rolesManager.CreateAsync(new IdentityRole(role));
    }
}

app.Run();


