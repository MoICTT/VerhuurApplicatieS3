using Microsoft.EntityFrameworkCore;
using VerhuurApplicatieAPI.Data;
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

// CORS — sta de Vue frontend toe
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueFrontend", policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod());
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
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
