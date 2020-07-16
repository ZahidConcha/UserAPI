using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserAPI_UI.Contractas;
using UserAPI_UI.ViewModels;

namespace UserAPI_UI.Services
{
    public class EstructuraRepo:BaseRepo<EstructuraVM>,IEstructuraRepo
    {
        private readonly IHttpClientFactory client;

        public EstructuraRepo(IHttpClientFactory client):base(client)
        {
            this.client = client;
        }

    }
}
