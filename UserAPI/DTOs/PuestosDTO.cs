﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.DTOs
{

    public class PuestosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        

    }
    public class PuestosCreateDTO
    {
        
        public string Nombre { get; set; }
        public string Clave { get; set; }

       
    }

}
