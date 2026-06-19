using Microsoft.AspNetCore.SignalR;

namespace VerhuurApplicatieAPI.Hubs;

public class AutoHub : Hub
{
    // Clients verbinden automatisch bij het openen van de pagina.
    // De server broadcast via IHubContext<AutoHub> vanuit de service.
}
