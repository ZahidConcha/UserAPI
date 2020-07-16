using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI_UI.Utilities
{
    public static class EndPoints
    {
        public static string BaseUrl = "https://localhost:44344/";
        public static string RegisterEndPoint = $"{BaseUrl}api/users/register";
        public static string LoginEndPoint = $"{BaseUrl}api/users/login";
        public static string EmpleadosEndPoint = $"{BaseUrl}api/Empleados/";
        public static string DepartentosEndPoint = $"{BaseUrl}api/Departamentos/";
        public static string PuuestosEndPoint = $"{BaseUrl}api/Puestos/";
        public static string SitiosEndPoint = $"{BaseUrl}api/Sitios/";
        public static string EstrucutraEndPoint = $"{BaseUrl}api/estructura/";
    }
}
