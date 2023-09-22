namespace MobileStore.Common.Abstractions.Services
{
    // read current user id
    public interface IReadIdentityService
    {
        Guid? UserId { get; }
    }
}
