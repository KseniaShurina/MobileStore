namespace MobileStore.Common.Models;

public class IdentityState
{
    public Guid UserId { get; set; }

    internal IdentityState(Guid userId)
    {
        UserId = userId;
    }
}