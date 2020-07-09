using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;
using UserAPI.Contracts;
using UserAPI.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManeger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;
        private readonly ILoggerService logger;

        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IConfiguration config, IUserRepository userRepository, ILoggerService logger)
        {
            this.signInManeger = signInManager;
            this.userManager = userManager;
            this.config = config;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all the users does not have Dto so it returns all the info of every user
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize (Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location} Get all Users Request Attempted");
                var users = await userRepository.FindAll();
                if (users == null)
                {
                    logger.LogWarn($"{location} Could not Get Data");
                    return NotFound();
                }
                logger.LogInfo($"{location} Data retured Succesfully");
                return Ok(users);
            }
            catch (Exception e)
            {
                return internalError($"{e.Message}-{e.InnerException}");
            }

        }



        /// <summary>
        /// Posts a token for a user that exists an can authenticate its self
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var location = GetControllerActionNames();
            try
            {

                var userName = userDTO.EmailAddress;
                var password = userDTO.Password;
                logger.LogInfo($"{location}:Login Attempt From User {userName}");
                var result = await signInManeger.PasswordSignInAsync(userName, password, false, false);

                if (result.Succeeded)
                {
                    logger.LogInfo($"{location}:Succesfully Authenticated {userName}");
                    var user = await userManager.FindByNameAsync(userName);
                    var tokenString = await GenorateJWT(user);
                    return Ok(new { token = tokenString });
                }
                logger.LogInfo($"{location}:Not Authenticated {userName}");
                return Unauthorized(userDTO);
            }
            catch (Exception e)
            {
                return internalError($"{e.Message}-{e.InnerException}");
            }

        }


        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [Route("register")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                logger.LogInfo($"{location}Register User Attempt {userDTO.EmailAddress}");
                var user = new IdentityUser
                {
                    Email = userDTO.EmailAddress,
                    UserName = userDTO.EmailAddress
                };
                var result = await userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        logger.LogError($"{location}:User Registration Failed {error.Code} - {error.Description}");
                    }
                    return internalError(($"{location}:Register Attempt From User {userDTO.EmailAddress}  failed"));
                }
                logger.LogInfo($"{location}Register User Attempt Succesfull {userDTO.EmailAddress}");
                return Ok(new { result.Succeeded });
            }
            catch (Exception)
            {

                throw;
            }




        }

        private async Task<string> GenorateJWT(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),

            };
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"], claims, null, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
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


