using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI_UI.Utilities
{
    public static class EndPoints
    {
        public static string BaseUrl = "https://localhost:44344/";
        public static string EmpleadosEndPoint = $"{BaseUrl}api/Empleados/";
        public static string SitiosEndPoint = $"{BaseUrl}api/Departamentos/";
        public static string PuestosEndPoint = $"{BaseUrl}api/Puestos/";
    }
}
