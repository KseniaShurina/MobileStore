namespace MobileStore.Common.Identity;

public class IdentityState
{
    private static readonly AsyncLocal<IdentityState?> CurrentLocal = new();

    /// <summary>
    /// Gets or sets the current operation (IdentityState) for the current thread.
    /// This flows across async calls.
    /// </summary>
    public static IdentityState? Current => CurrentLocal.Value;

    public static void SetCurrent(int userId)
    {
        CurrentLocal.Value = new IdentityState(userId);
    }

    public int UserId { get; set; }

    private IdentityState(int userId)
    {
        UserId = userId;
    }
}