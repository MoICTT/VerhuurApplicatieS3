using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;
using VerhuurApplicatieAPI.DTOs;
using VerhuurApplicatieAPI.Hubs;
using VerhuurApplicatieAPI.Models;
using VerhuurApplicatieAPI.Repositories;
using VerhuurApplicatieAPI.Services;

namespace Backend.Tests.Services;

public class ReservatieServiceTests
{
    private readonly Mock<IReservatieRepository> _reservatieRepo = new();
    private readonly Mock<IAutoRepository> _autoRepo = new();
    private readonly Mock<IKlantRepository> _klantRepo = new();
    private readonly Mock<IHubContext<AutoHub>> _hubContext = new();

    private ReservatieService CreateService()
    {
        var mockClients = new Mock<IHubClients>();
        var mockClientProxy = new Mock<IClientProxy>();
        mockClients.Setup(c => c.All).Returns(mockClientProxy.Object);
        _hubContext.Setup(h => h.Clients).Returns(mockClients.Object);
        return new(_reservatieRepo.Object, _autoRepo.Object, _klantRepo.Object, _hubContext.Object);
    }

    [Fact]
    public async Task MaakReservatieAsync_AutoNietGevonden_ThrowsKeyNotFoundException()
    {
        _autoRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Auto?)null);

        var service = CreateService();
        var dto = new ReservatieAanmakenDto { AutoId = 99 };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.MaakReservatieAsync(dto));
    }

    [Fact]
    public async Task MaakReservatieAsync_AutoNietBeschikbaar_ThrowsInvalidOperationException()
    {
        _autoRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Auto { Id = 1, Beschikbaar = false });

        var service = CreateService();
        var dto = new ReservatieAanmakenDto { AutoId = 1 };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.MaakReservatieAsync(dto));
    }

    [Fact]
    public async Task MaakReservatieAsync_EindDatumVoorStartDatum_ThrowsArgumentException()
    {
        _autoRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Auto { Id = 1, Beschikbaar = true });

        var service = CreateService();
        var dto = new ReservatieAanmakenDto
        {
            AutoId = 1,
            StartDatum = new DateTime(2026, 7, 10),
            EindDatum = new DateTime(2026, 7, 5)
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.MaakReservatieAsync(dto));
    }

    [Fact]
    public async Task MaakReservatieAsync_GeldigeData_BerektTotaalPrijsCorrect()
    {
        var auto = new Auto { Id = 1, Merk = "Toyota", Model = "Yaris", PrijsPerDag = 50m, Beschikbaar = true };
        var klant = new Klant { Id = 1, Voornaam = "Jan", Achternaam = "Janssen", Email = "jan@test.nl" };
        var reservatie = new Reservatie { Id = 1, AutoId = 1, KlantId = 1 };

        _autoRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(auto);
        _klantRepo.Setup(r => r.GetByEmailAsync("jan@test.nl")).ReturnsAsync(klant);
        _reservatieRepo.Setup(r => r.AddAsync(It.IsAny<Reservatie>())).ReturnsAsync(reservatie);

        var service = CreateService();
        var dto = new ReservatieAanmakenDto
        {
            AutoId = 1,
            Voornaam = "Jan",
            Achternaam = "Janssen",
            Email = "jan@test.nl",
            StartDatum = new DateTime(2026, 7, 1),
            EindDatum = new DateTime(2026, 7, 4)
        };

        var result = await service.MaakReservatieAsync(dto);

        Assert.Equal(150m, result.TotaalPrijs); // 3 dagen * €50
    }

    [Fact]
    public async Task MaakReservatieAsync_NieuweKlant_WordtAangemaakt()
    {
        var auto = new Auto { Id = 1, Beschikbaar = true, PrijsPerDag = 40m };
        var nieuweKlant = new Klant { Id = 2, Voornaam = "Fatima", Achternaam = "El Amrani", Email = "fatima@test.nl" };
        var reservatie = new Reservatie { Id = 5, AutoId = 1, KlantId = 2 };

        _autoRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(auto);
        _klantRepo.Setup(r => r.GetByEmailAsync("fatima@test.nl")).ReturnsAsync((Klant?)null);
        _klantRepo.Setup(r => r.AddAsync(It.IsAny<Klant>())).ReturnsAsync(nieuweKlant);
        _reservatieRepo.Setup(r => r.AddAsync(It.IsAny<Reservatie>())).ReturnsAsync(reservatie);

        var service = CreateService();
        var dto = new ReservatieAanmakenDto
        {
            AutoId = 1,
            Voornaam = "Fatima",
            Achternaam = "El Amrani",
            Email = "fatima@test.nl",
            StartDatum = new DateTime(2026, 8, 1),
            EindDatum = new DateTime(2026, 8, 3)
        };

        var result = await service.MaakReservatieAsync(dto);

        _klantRepo.Verify(r => r.AddAsync(It.IsAny<Klant>()), Times.Once);
        Assert.Equal("Fatima", result.KlantVoornaam);
    }
}
