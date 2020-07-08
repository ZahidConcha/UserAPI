using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserAPI.Contracts;
using UserAPI.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        private readonly IUserRepository userRepository;

        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IConfiguration config, IUserRepository userRepository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets all the users does not have Dto so it returns all the info of every user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var users = await userRepository.FindAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }



       /// <summary>
       /// Posts a token for a user that exists an can authenticate its self
       /// </summary>
       /// <param name="userDTO"></param>
       /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {

            var result = await signInManager.PasswordSignInAsync(userDTO.EmailAddress, userDTO.Password, false, false);

            if(result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(userDTO.EmailAddress);
                var tokenString = await GenereteJSONToken(user);
                return Ok(new { token = tokenString });
                //return Ok(user);
            }
            return Unauthorized(userDTO);
        }



        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [Route("register")]
        [Authorize (Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {

            var user = new IdentityUser
            {
                Email = userDTO.EmailAddress,
                UserName = userDTO.EmailAddress
            };
            var result = await userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(new { result.Succeeded });


        }

        private async Task<string> GenereteJSONToken(IdentityUser user)
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

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"], claims, null, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }





    }
}
