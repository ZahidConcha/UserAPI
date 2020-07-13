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
            CreateMap<Departamento,DepartamentosDTO>().ReverseMap();
            CreateMap<Departamento, DepartamentosCreateDTO>().ReverseMap();
            CreateMap<Puesto, PuestosDTO>().ReverseMap();
            CreateMap<Puesto, PuestoCreateDTO>().ReverseMap();
            CreateMap<Sitios, SitiosDTO>().ReverseMap();
            CreateMap<Sitios, SitiosCreateDTO>().ReverseMap();

        }
    }
}
