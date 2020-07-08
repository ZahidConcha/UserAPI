using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Contracts;
using UserAPI.DTOs;
using UserAPI.Modals;

namespace UserAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosRepo empleadosRepo;
        private readonly IMapper mapper;

        public EmpleadosController(IEmpleadosRepo empleadosRepo,IMapper mapper)
        {
            this.empleadosRepo = empleadosRepo;
            this.mapper = mapper;
        }




        /// <summary>
        /// Gets all the workers Information
        /// </summary>
        /// <returns>List of Workers</returns>
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


        /// <summary>
        /// Gets a worker by it id
        /// </summary>
        /// <param name="id"> Worker Id</param>
        /// <returns>Information of the worker with the same Id</returns>
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

        /// <summary>
        /// Creates New Worker 
        /// </summary>
        /// <param name="empleado">Model worker with its properties!</param>
        /// <returns>Created Code</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEmpleado([FromBody] EmpleadosDTO empleado)
        {
            if (empleado == null)
            {
                return BadRequest(empleado);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(empleado);
            }
            var empleadoInfo = mapper.Map<Empleado>(empleado);
            var isSuccess = await empleadosRepo.Create(empleadoInfo);
            if (!isSuccess)
            {
                return Conflict();
            }
            return Created("Created", new { empleadoInfo });
        }

        /// <summary>
        /// Updates an existing user! 
        /// </summary>
        /// <param name="id">Takes a user id as a param to search for a user id in the database</param>
        /// <param name="empleado">information o one user with a match on the given id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes a user bye its id
        /// </summary>
        /// <param name="id">Recives a id</param>
        /// <returns> 202 no content</returns>
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
