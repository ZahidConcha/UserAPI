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
            CreateMap<Departamentos, DepartamentosCreateDTO>().ReverseMap();
            CreateMap<Puestos, PuestosDTO>().ReverseMap();
            CreateMap<Puestos, PuestosCreateDTO>().ReverseMap();
            CreateMap<Sitios, SitiosDTO>().ReverseMap();
            CreateMap<Sitios, SitiosCreateDTO>().ReverseMap();
            CreateMap<Estructura, EstructuraDTO>().ReverseMap();
            CreateMap<Estructura, EstucturaCreateDTO>().ReverseMap();



        }
    }
}
