using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

namespace MobileStore.Presentation.Blazor.Components.Standard.Menu
{
    public class XStNavLink : MudNavLink
    {
        public XStNavLink()
        {
            DisableRipple = true;
            Match = NavLinkMatch.All;
        }
    }
}
