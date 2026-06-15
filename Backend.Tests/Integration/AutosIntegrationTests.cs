using System.Net;
using Xunit;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VerhuurApplicatieAPI.Data;

namespace Backend.Tests.Integration;

public class AutosIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AutosIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Verwijder SQL Server configuratie volledig (EF Core 9 registreert meerdere descriptors)
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<AppDbContext>>();

                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                // Seed testdata
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
                db.SeedTestData();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GET_autos_geeft_status200()
    {
        var response = await _client.GetAsync("/api/autos");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_autos_geeft_json_terug()
    {
        var response = await _client.GetAsync("/api/autos");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GET_autos_bevat_seeded_autos()
    {
        var response = await _client.GetAsync("/api/autos");
        var json = await response.Content.ReadAsStringAsync();
        var autos = JsonSerializer.Deserialize<JsonElement[]>(json);

        Assert.NotNull(autos);
        Assert.True(autos.Length > 0);
    }

    [Fact]
    public async Task GET_autos_bevat_verwachte_velden()
    {
        var response = await _client.GetAsync("/api/autos");
        var json = await response.Content.ReadAsStringAsync();
        var autos = JsonSerializer.Deserialize<JsonElement[]>(json);

        var eerste = autos![0];
        Assert.True(eerste.TryGetProperty("merk", out _), "Veld 'merk' ontbreekt");
        Assert.True(eerste.TryGetProperty("model", out _), "Veld 'model' ontbreekt");
        Assert.True(eerste.TryGetProperty("prijsPerDag", out _), "Veld 'prijsPerDag' ontbreekt");
        Assert.True(eerste.TryGetProperty("beschikbaar", out _), "Veld 'beschikbaar' ontbreekt");
    }
}
