namespace MobileStore.Presentation.Blazor.Helpers
{
    public static class ContentHelper
    {
        public static string GetUrl(Guid contentId)
        {
            return $"/api/contents/{contentId}";
        }
    }
}
