using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.CircularGauge;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserAPI_UI.Providers
{
    public class APIAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public APIAuthenticationStateProvider(ILocalStorageService localStorage,JwtSecurityTokenHandler tokenHandler)
        {
            this.localStorage = localStorage;
            this.tokenHandler = tokenHandler;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await localStorage.GetItemAsync<string>("AuthenticationToken");
                if (string.IsNullOrWhiteSpace(savedToken))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                var tokenContent = tokenHandler.ReadJwtToken(savedToken);
                var expiry = tokenContent.ValidTo;
                if (expiry < DateTime.Now)
                {
                    await localStorage.RemoveItemAsync("AuthenticationToken");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                //Get Claims From Token
                var claims = ParseClaims(tokenContent);
                //Build Author user Object
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                //return Authenticated State person
                return new AuthenticationState(user);

            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task LoggedIn()
        {
            var savedToken = await localStorage.GetItemAsync<string>("AuthenticationToken");
            var tokenContent = tokenHandler.ReadJwtToken(savedToken);
            var claims = ParseClaims(tokenContent);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authenticationState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authenticationState);
        }

        public void LoggeOut()
        {
            var UnAuthenticatedState = new ClaimsPrincipal(new ClaimsIdentity());
            var authenticationState = Task.FromResult(new AuthenticationState(UnAuthenticatedState));
            NotifyAuthenticationStateChanged(authenticationState);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
