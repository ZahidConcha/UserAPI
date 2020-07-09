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
    public class SitiosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILoggerService logger;
        private readonly ISitiosRepo sitiosRepo;

        public SitiosController(IMapper mapper, ILoggerService logger,ISitiosRepo sitiosRepo)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.sitiosRepo = sitiosRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var location = GetLocation();
            try
            {
                logger.LogInfo($"{location} Attempted Get Request");
                var sitios = await sitiosRepo.FindAll();
                if (sitios == null)
                {
                    logger.LogWarn($"{location} No data Found");
                    return NotFound();
                }
                logger.LogInfo($"{location}: Attempted Call Succesful");
                return Ok(sitios);
            }
            catch (Exception e)
            {
                return InternalError($"{location}:{e.Message}-{e.InnerException}");
            }
       
        }


       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var location = GetLocation();
            try
            {
                logger.LogInfo($"{location} Attempted Get by Id Request Id:{id}");
                var puesto = await sitiosRepo.FindById(id);
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
                return InternalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

     
        [HttpPost]
        public async Task<IActionResult> Createpuesto([FromBody]SitiosDTO sitio)
        {
            var location = GetLocation();
            try
            {
                logger.LogInfo($"{location} Attempted Post Request (Create)");
                if (sitio == null)
                {
                    logger.LogWarn($"{location} Missing Data");
                    return BadRequest(sitio);
                }
                if (!ModelState.IsValid)
                {
                    logger.LogWarn($"{location} Data invalid");
                    return BadRequest(sitio);
                }
                var SitiosInfo = mapper.Map<Sitios>(sitio);
                var isSuccess = await sitiosRepo.Create(SitiosInfo);
                if (!isSuccess)
                {
                    logger.LogError($"{location} Something went wrong while creating");
                    return Conflict();
                }
                logger.LogInfo($"{location} Create Attempt Succesful");
                return Created("Created", new { sitio });
            }
            catch (Exception e)
            {
                return InternalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

       
        [HttpPut("{id}")]
       
        public async Task<IActionResult> UpDate(int id, [FromBody] SitiosDTO sitios)
        {
            var location = GetLocation();
            try
            {
                logger.LogInfo($"{location} Update Attempt");
                if (id < 1 || sitios == null || id != sitios.Id)
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
                var sitiosInfo = mapper.Map<Sitios>(sitios);
                var isSuccess = await sitiosRepo.Update(sitiosInfo);
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
                return InternalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }

       
        [HttpDelete("{id}")]
      
        public async Task<IActionResult> Delete(int id)
        {
            var location = GetLocation();
            try
            {
                logger.LogInfo($"{location} Delete Attempt");
                if (id < 1)
                {
                    logger.LogWarn($"{location} Ivalid Id {id}");
                    return BadRequest();
                }
                var puesto = await sitiosRepo.Exsists(id);
                if (!puesto)
                {
                    logger.LogWarn($"{location} Faild to find User Id {id}");
                    return NotFound();
                }

                var delsitios = await sitiosRepo.FindById(id);
                var isSuccess = await sitiosRepo.Delete(delsitios);
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
                return InternalError($"{location}:{e.Message}-{e.InnerException}");
            }

        }


        private string GetLocation()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string messege)
        {
            logger.LogError($"{messege}");
            return StatusCode(500, "Server error");
        }
    }
}
