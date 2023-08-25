using Microsoft.AspNetCore.Authorization;
using MobileStore.Common.Identity;

namespace MobileStore.Presentation.Mvc.Controllers.Base
{
    [Authorize]
    public  abstract class MvcControllerBaseSecure : MvcControllerBase
    {
        //из текущего авторизованного юзера достаёт ид
        public Guid UserId => IdentityState.Current!.UserId;
    }
}
