using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserPanel.Auth;
using UserPanel.Auth.Services;
using UserPanel.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // Optional: Clear default providers
builder.Logging.AddConsole(); // Add console logging
builder.Logging.AddDebug(); // Add debug logging

// Register the TokenService
builder.Services.AddScoped<UserPanel.Auth.Services.ITokenService, TokenService>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("ITokenService has been registered successfully.");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
