using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MobileStore.Controllers.Base
{
    [Authorize]
    public  abstract class MvcControllerBaseSecure : MvcControllerBase
    {
        //из текущего авторизованного юзера достаёт ид
        public int UserId => int.Parse(User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
    }
}
