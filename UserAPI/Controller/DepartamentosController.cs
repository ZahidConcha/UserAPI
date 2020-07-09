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
    public class DepartamentosController : ControllerBase
    {
        private readonly ILoggerService logger;
        private readonly IDepartamentoRepo departamentoRepo;
        private readonly IMapper mapper;

        public DepartamentosController(ILoggerService logger,IDepartamentoRepo departamentoRepo, IMapper mapper)
        {
            this.logger = logger;
            this.departamentoRepo = departamentoRepo;
            this.mapper = mapper;
        }
        /// <summary>
        /// Gets all the workstations Information
        /// </summary>
        /// <returns>List of Workers</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartamentos()
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Get Request");
                var puestos = await departamentoRepo.FindAll();
                if (puestos == null)
                {
                    logger.LogWarn($"{location} No data Found");
                    return NotFound();
                }
                logger.LogInfo($"{location}: Attempted Call Succesful");
                return Ok(puestos);
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }


        /// <summary>
        /// Gets a workstation by id
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
                var puesto = await departamentoRepo.FindById(id);
                if (puesto == null)
                {
                    logger.LogWarn($"{location} Faild to Find Id{id}");
                    return NotFound();
                }
                logger.LogInfo($"{location}: Attempted Call Succesful");
                return Ok(puesto);
            }

            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

        /// <summary>
        /// Creates New workstation
        /// </summary>
        /// <param name="departamento">Model worker with its properties!</param>
        /// <returns>Created Code</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Createpuesto([FromBody] DepartamentosDTO departamento)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Post Request (Create)");
                if (departamento == null)
                {
                    logger.LogWarn($"{location} Missing Data");
                    return BadRequest(departamento);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location} Data invalid");
                    return BadRequest(departamento);
                }
                var departamentoInfo = mapper.Map<Departamentos>(departamento);
                var isSuccess = await departamentoRepo.Create(departamentoInfo);
                if (!isSuccess)
                {
                    logger.LogError($"{location} Something went wrong while creating");
                    return Conflict();
                }
                logger.LogInfo($"{location} Create Attempt Succesful");
                return Created("Created", new { departamento });
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

        /// <summary>
        /// Updates an existing WorkStation! 
        /// </summary>
        /// <param name="id">Takes a user id as a param to search for a user id in the database</param>
        /// <param name="departamentos">information o one user with a match on the given id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpDate(int id, [FromBody] DepartamentosDTO departamentos)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Update Attempt");
                if (id < 1 || departamentos == null || id != departamentos.Id)
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
                var departamentoInfo = mapper.Map<Departamentos>(departamentos);
                var isSuccess = await departamentoRepo.Update(departamentoInfo);
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
        /// Deletes a Workstation bye its id
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
                var puesto = await departamentoRepo.Exsists(id);
                if (!puesto)
                {
                    logger.LogWarn($"{location} Faild to find User Id {id}");
                    return NotFound();
                }

                var deldepartamentos = await departamentoRepo.FindById(id);
                var isSuccess = await departamentoRepo.Delete(deldepartamentos);
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
