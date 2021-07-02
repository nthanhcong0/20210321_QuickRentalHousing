using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace QuickRentalHousing.FE.Handlers
{
    public class QuickRentalHouseApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public QuickRentalHouseApiAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:5001" },
                scopes: new[] { "QuickRentalHousing.Api" });
        }
    }
}
