namespace MobileStore.Common.Identity;

public class IdentityState
{
    private static readonly AsyncLocal<IdentityState?> CurrentLocal = new();

    /// <summary>
    /// Gets or sets the current operation (IdentityState) for the current thread.
    /// This flows across async calls.
    /// </summary>
    public static IdentityState? Current => CurrentLocal.Value;

    public static void SetCurrent(Guid userId)
    {
        CurrentLocal.Value = new IdentityState(userId);
    }

    public Guid UserId { get; set; }

    private IdentityState(Guid userId)
    {
        UserId = userId;
    }
}