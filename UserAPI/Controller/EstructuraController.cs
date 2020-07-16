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
    public class EstructuraController : ControllerBase
    {
        private readonly ILoggerService logger;
        private readonly IMapper mapper;
        private readonly IEstructuraRepo estructuraRepo;

        public EstructuraController(ILoggerService logger, IMapper mapper,IEstructuraRepo estructuraRepo)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.estructuraRepo = estructuraRepo;
        }
      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartamentos()
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Get Request");
                var puestos = await estructuraRepo.FindAll();
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
                var puesto = await estructuraRepo.FindById(id);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Createpuesto([FromBody] EstucturaCreateDTO estuctura)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Attempted Post Request (Create)");
                if (estuctura == null)
                {
                    logger.LogWarn($"{location} Missing Data");
                    return BadRequest(estuctura);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location} Data invalid");
                    return BadRequest(estuctura);
                }
                var estructuraInfo = mapper.Map<Estructura>(estuctura);
                var isSuccess = await estructuraRepo.Create(estructuraInfo);
                if (!isSuccess)
                {
                    logger.LogError($"{location} Something went wrong while creating");
                    return Conflict();
                }
                logger.LogInfo($"{location} Create Attempt Succesful");
                return Created("Created", new { estructuraInfo });
            }
            catch (Exception e)
            {
                return internalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

     
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpDate(int id, [FromBody] EstructuraDTO estructura)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Update Attempt");
                if (id < 1 || estructura == null || id != estructura.Id)
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
                var estructuraInfo = mapper.Map<Estructura>(estructura);
                var isSuccess = await estructuraRepo.Update(estructuraInfo);
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
                var puesto = await estructuraRepo.Exsists(id);
                if (!puesto)
                {
                    logger.LogWarn($"{location} Faild to find User Id {id}");
                    return NotFound();
                }

                var deldepartamentos = await estructuraRepo.FindById(id);
                var isSuccess = await estructuraRepo.Delete(deldepartamentos);
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
