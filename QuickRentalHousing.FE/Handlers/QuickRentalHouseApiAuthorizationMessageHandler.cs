using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace QuickRentalHousing.FE.Handlers
{
    public class QuickRentalHouseApiAuthorizationMessageHandler
    {
        public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
        {
            public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
                NavigationManager navigationManager)
                : base(provider, navigationManager)
            {
                ConfigureHandler(
                    authorizedUrls: new[] { "https://localhost:5016" },
                    scopes: new[] { "weather.read" });
            }
        }
    }
}
