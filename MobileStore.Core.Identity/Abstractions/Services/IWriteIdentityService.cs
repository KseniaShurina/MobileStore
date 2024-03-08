using System;
using System.Security.Claims;

namespace MobileStore.Common.Abstractions.Services
{
    // write current user session
    public interface IWriteIdentityService
    {
        void SetCurrent(ClaimsPrincipal principal);

        void ClearCurrent();
    }
}
