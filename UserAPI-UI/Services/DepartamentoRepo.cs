using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserAPI_UI.Contractas;
using UserAPI_UI.ViewModels;

namespace UserAPI_UI.Services
{
    public class DepartamentoRepo : BaseRepo<DepartamentoVM> , IDepartamentoRepo
    {

        private readonly IHttpClientFactory client;

        public DepartamentoRepo(IHttpClientFactory client) : base(client)
        {
            this.client = client;
        }
    }
}
