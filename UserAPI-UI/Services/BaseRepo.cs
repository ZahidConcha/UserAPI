using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserAPI_UI.Contractas;

namespace UserAPI_UI.Services
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly IHttpClientFactory clientFactory;

        public BaseRepo(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<bool> Create(string url, T entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (entity == null)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(entity),Encoding.UTF8,"application/json" );

            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public Task<bool> Delete(string url, T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<T>> GetAll(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }
            return null;
        }

        public Task<T> GetbyId(string url, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string url, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
