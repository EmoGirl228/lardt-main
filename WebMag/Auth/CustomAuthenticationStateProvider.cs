using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace WebMag.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ProtectedLocalStorage _localStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        
        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, ProtectedLocalStorage localStorage)
        {
            _sessionStorage = sessionStorage; 
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
               try
               {
                   var UserSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
                   var UserSession = UserSessionStorageResult.Success ? UserSessionStorageResult.Value : null;
           
                   if (UserSession == null) 
                   {
                       return await Task.FromResult(new AuthenticationState(_anonymous));
                   }

                   var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                   {
                      new Claim(ClaimTypes.Sid, UserSession.Id),
                      new Claim(ClaimTypes.Name, UserSession.Name),
                      new Claim(ClaimTypes.Email, UserSession.Email),
                      new Claim(ClaimTypes.Role, UserSession.Role)
                   }, "CustomAuth"));
                   return await Task.FromResult(new AuthenticationState(claimPrincipal));
               }
               catch
               {
                   return await Task.FromResult(new AuthenticationState(_anonymous));
               }
        }
        public async Task UpdateAuthenticationStateAsync(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _localStorage.SetAsync("LocalUserSession", userSession);
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                   new Claim(ClaimTypes.Sid, userSession.Id),
                   new Claim(ClaimTypes.Name, userSession.Name),
                   new Claim(ClaimTypes.Email, userSession.Email),
                   new Claim(ClaimTypes.Role, userSession.Role)
                }));
            }
            else
            {
                await _localStorage.DeleteAsync("LocalUserSession");
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
    
}
