using Syncfusion.Blazor.CircularGauge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI_UI.ViewModels;

namespace UserAPI_UI.Contractas
{
    public interface IAuthenticationRepo
    {
        public Task<bool> Register(RegistratioVM user);
        public Task<bool> Login(LoginVM user);
        public Task Logout();
    }
}
