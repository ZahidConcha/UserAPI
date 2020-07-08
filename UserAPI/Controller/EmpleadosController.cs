using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
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
        private readonly ILoggerService logger;

        public EmpleadosController(IEmpleadosRepo empleadosRepo,IMapper mapper, ILoggerService logger)
        {
            this.empleadosRepo = empleadosRepo;
            this.mapper = mapper;
            this.logger = logger;
        }




        /// <summary>
        /// Gets all the workers Information
        /// </summary>
        /// <returns>List of Workers</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmpleados()
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Get Request");
                var empleados = await empleadosRepo.FindAll();
                if (empleados == null)
                {
                    logger.LogWarn($"{location} Faild to Get Data");
                    return NotFound();
                }
                logger.LogInfo($"{location}: Attempet Call Succesful");
                return Ok(empleados);
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }
           
        }


        /// <summary>
        /// Gets a worker by it id
        /// </summary>
        /// <param name="id"> Worker Id</param>
        /// <returns>Information of the worker with the same Id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Get by Id Request Id:{id}");
                var empleado = await empleadosRepo.FindById(id);
                if (empleado == null)
                {
                    logger.LogWarn($"{location} Faild to Find Id{id}");
                    return NotFound();
                }
                logger.LogInfo($"{location}: Attempet Call Succesful");
                return Ok(empleado);
            }
             
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }
           
        }

        /// <summary>
        /// Creates New Worker 
        /// </summary>
        /// <param name="empleado">Model worker with its properties!</param>
        /// <returns>Created Code</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateEmpleado([FromBody] EmpleadosDTO empleado)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Post Request (Create)");
                if (empleado == null)
                {
                    logger.LogWarn($"{location} Missing Data");
                    return BadRequest(empleado);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location} Data invalid");
                    return BadRequest(empleado);
                }
                var empleadoInfo = mapper.Map<Empleado>(empleado);
                var isSuccess = await empleadosRepo.Create(empleadoInfo);
                if (!isSuccess)
                {
                    logger.LogError($"{location} Something went wrong while creating");
                    return Conflict();
                }
                logger.LogInfo($"{location} Create Attempt Succesful");
                return Created("Created", new { empleadoInfo });
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }
          
        }

        /// <summary>
        /// Updates an existing user! 
        /// </summary>
        /// <param name="id">Takes a user id as a param to search for a user id in the database</param>
        /// <param name="empleado">information o one user with a match on the given id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpDate(int id, [FromBody] Empleado empleado)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Update Attempt");
                if (id < 1 || empleado == null || id != empleado.Id)
                {
                    logger.LogWarn($"{location} Faild to Find or Invalid Id{id}");
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location} Data Invalid");
                    return BadRequest(ModelState);
                }
                logger.LogInfo($"{location} Found Id{id}");
                var isSuccess = await empleadosRepo.Update(empleado);
                if (!isSuccess)
                {
                    logger.LogWarn($"{location} Something went wrong while Updating");
                    return Conflict();
                }
                logger.LogInfo($"{location} Update Attempt Succesfull");
                return NoContent();
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

        /// <summary>
        /// Deletes a user bye its id
        /// </summary>
        /// <param name="id">Recives a id</param>
        /// <returns> 202 no content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Delete Attempt");
                if (id < 1)
                {
                    logger.LogWarn($"{location} Ivalid Id {id}");
                    return BadRequest();
                }
                var empleado = await empleadosRepo.Exsists(id);
                if (!empleado)
                {
                    logger.LogWarn($"{location} Faild to find User Id {id}");
                    return NotFound();
                }

                var delEmpleado = await empleadosRepo.FindById(id);
                var isSuccess = await empleadosRepo.Delete(delEmpleado);
                if (!isSuccess)
                {
                    logger.LogError($"{location}  Something went wrong while Deleting");
                    return Conflict();
                }
                logger.LogInfo($"{location} Delete Attempt Succesful");
                return NoContent();
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }


        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller} - {action}";
        }


        private ObjectResult internalError(string message)
        {
            logger.LogError($"{message}");
            return StatusCode(500, "Server error");
        }
    }
}
