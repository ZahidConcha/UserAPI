using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UserAPI_UI.Contractas;
using UserAPI_UI.Providers;
using UserAPI_UI.Utilities;
using UserAPI_UI.ViewModels;

namespace UserAPI_UI.Services
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationRepo(IHttpClientFactory clientFactory,ILocalStorageService localStorage,AuthenticationStateProvider authenticationStateProvider)
        {
            this.clientFactory = clientFactory;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Login(LoginVM user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, EndPoints.LoginEndPoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);

            //store Token
            await localStorage.SetItemAsync("AuthenticationToken", token.Token);

            //Change Authentication State
            await ((APIAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
            return true;
            
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("AuthenticationToken");
            ((APIAuthenticationStateProvider)authenticationStateProvider).LoggeOut();
        }

        public async Task<bool> Register(RegistratioVM user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, EndPoints.RegisterEndPoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
