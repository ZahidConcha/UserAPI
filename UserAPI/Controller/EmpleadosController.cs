using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Contracts;
using UserAPI.Modals;

namespace UserAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosRepo empleadosRepo;

        public EmpleadosController(IEmpleadosRepo empleadosRepo)
        {
            this.empleadosRepo = empleadosRepo;
        }





        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            var empleados = await empleadosRepo.FindAll();
            if (empleados == null)
            {
                return NotFound();
            }
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empleado = await empleadosRepo.FindById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmpleado([FromBody] Empleado empleado)
        {
            if (empleado == null)
            {
                return BadRequest(empleado);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(empleado);
            }
            var isSuccess = await empleadosRepo.Create(empleado);
            if (!isSuccess)
            {
                return Conflict();
            }
            return Created("Created", new { empleado });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpDate(int id, [FromBody] Empleado empleado)
        {
            if (id < 1 || empleado == null || id != empleado.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isSuccess = await empleadosRepo.Update(empleado);
            if (!isSuccess)
            {
                return Conflict();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var empleado = await empleadosRepo.Exsists(id);
            if (!empleado)
            {
                return NotFound();
            }

            var delEmpleado = await empleadosRepo.FindById(id);
            var isSuccess = await empleadosRepo.Delete(delEmpleado);
            if (!isSuccess)
            {
                return Conflict();
            }

            return NoContent();
        }
    }
}
