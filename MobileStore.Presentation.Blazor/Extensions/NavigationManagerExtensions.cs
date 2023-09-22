using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.Web;

namespace MobileStore.Presentation.Blazor.Extensions
{
    public static class NavigationManagerExtensions
    {
        // Get entire querystring name/value collection
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        // Get single querystring value with specified key
        public static string? QueryString(this NavigationManager navigationManager, string key)
        {
            var items = navigationManager.QueryString();
            return items[key];
        }
    }
}
