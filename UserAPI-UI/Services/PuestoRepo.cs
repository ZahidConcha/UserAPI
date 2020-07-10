using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserAPI_UI.Contractas;
using UserAPI_UI.Pages;

namespace UserAPI_UI.Services
{
    public class PuestoRepo : BaseRepo<Puesto>
    {
        private readonly IHttpClientFactory client;

        public PuestoRepo(IHttpClientFactory client) : base(client)
        {
            this.client = client;
        }
    }
}
