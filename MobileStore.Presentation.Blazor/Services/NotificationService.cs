using MudBlazor;

namespace MobileStore.Presentation.Blazor.Services
{
    public class NotificationService
    {
        private readonly ISnackbar _snackBar;

        public NotificationService(ISnackbar snackBar)
        {
            _snackBar = snackBar;
        }

        public void ShowSuccess(string message)
        {
            _snackBar.Add(message, Severity.Success);
        }
    }
}
