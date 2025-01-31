using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Core.Contracts.Services;
using Core.Domain.Entities;

namespace PosPlusClient.Services
{
    public enum UserAuthorizationState
    {
        Authoraizing = 0,
        Authorized = 1,
        UnAuthorized = 2
    }

    public class SimpleAuthenticationStateProvider(IServiceManager serviceManager, AppStateProvider appStateProvider)
        : AuthenticationStateProvider
    {
        private User? _currentUser;
        
        private AuthenticationState? _currentAuthenticationState;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = _currentUser != null
                ? new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, _currentUser.Username),
                        new Claim("PermissionsGranted", _currentUser.PermissionGrantedString()),
                        new Claim("PermissionsWithElevation", _currentUser.PermissionWithElevationString())
                    },
                    "SimpleAuth")
                : new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            _currentAuthenticationState = new AuthenticationState(user);
            return Task.FromResult(_currentAuthenticationState);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            _currentUser = await serviceManager.User.ValidateCredentials(username, password);
            appStateProvider.UserProfile = _currentUser;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return _currentUser != null;
        }

        public Task LogoutAsync()
        {
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.CompletedTask;
        }

        public Task<UserAuthorizationState> VerifyAuthorizationAsync(string? requiredPermission)
        {
            if (_currentUser.Isadmin)
            {
                return Task.FromResult(UserAuthorizationState.Authorized);   
            }

            if (_currentAuthenticationState == null || requiredPermission == null)
            {
                return Task.FromResult(UserAuthorizationState.UnAuthorized);
            }

            if (_currentAuthenticationState.User.HasClaim(c =>
                    c.Type == "PermissionsGranted" && c.Value.Split(',').Contains(requiredPermission)))
            {
                return Task.FromResult(UserAuthorizationState.Authorized);
            }

            if (_currentAuthenticationState.User.HasClaim(c =>
                    c.Type == "PermissionsWithElevation" && c.Value.Split(',').Contains(requiredPermission)))
            {
                return Task.FromResult(UserAuthorizationState.Authoraizing);
            }

            return Task.FromResult(UserAuthorizationState.UnAuthorized);
        }
    }
}