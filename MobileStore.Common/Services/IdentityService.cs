using MobileStore.Common.Abstractions.Services;
using System.Security.Claims;
using MobileStore.Common.Models;

namespace MobileStore.Common.Services
{
    internal class IdentityService : IIdentityService
    {
        private IdentityState? _current;

        public void SetCurrent(ClaimsPrincipal principal)
        {
            var userIdString = principal.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            _current = new IdentityState(Guid.Parse(userIdString));
        }

        public void ClearCurrent()
        {
            _current = null;
        }

        public Guid? UserId => _current?.UserId;
    }
}
