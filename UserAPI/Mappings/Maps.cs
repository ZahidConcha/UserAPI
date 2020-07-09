using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.DTOs;
using UserAPI.Modals;

namespace UserAPI.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Empleado, EmpleadosDTO>().ReverseMap();
            CreateMap<Departamentos,DepartamentosDTO>().ReverseMap();
            CreateMap<Puestos, PuestosDTO>().ReverseMap();
            CreateMap<Sitios, SitiosDTO>().ReverseMap();
        }
    }
}
