using Microsoft.AspNetCore.Authorization;

namespace MobileStore.Presentation.Mvc.Controllers.Base
{
    [Authorize]
    public abstract class MvcControllerBaseSecure : MvcControllerBase
    {

    }
}
