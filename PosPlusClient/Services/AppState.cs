using Core.Domain.Entities;

namespace PosPlusClient.Services;

public class AppState
{
    private User? _userProfile;

    public User? UserProfile
    {
        get => _userProfile;
        set
        {
            _userProfile = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}