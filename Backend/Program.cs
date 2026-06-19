using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VerhuurApplicatieAPI.Data;
using VerhuurApplicatieAPI.Hubs;
using VerhuurApplicatieAPI.Repositories;
using VerhuurApplicatieAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repositories
builder.Services.AddScoped<IAutoRepository, AutoRepository>();
builder.Services.AddScoped<IReservatieRepository, ReservatieRepository>();
builder.Services.AddScoped<IKlantRepository, KlantRepository>();

// Services
builder.Services.AddScoped<IAutoService, AutoService>();
builder.Services.AddScoped<IReservatieService, ReservatieService>();

// Controllers
builder.Services.AddControllers();

// SignalR
builder.Services.AddSignalR();

// JWT authenticatie
var jwtSecret = builder.Configuration["Jwt:Secret"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        };
    });

// CORS — sta de Vue frontend toe (AllowCredentials vereist voor SignalR WebSockets)
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueFrontend", policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

var app = builder.Build();

// Database aanmaken + seed data laden
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.IsRelational())
        db.Database.Migrate();
}

app.UseCors("VueFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<AutoHub>("/hubs/autos");

app.Run();

public partial class Program { }
