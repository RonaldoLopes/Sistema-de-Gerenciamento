
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SG.Domain.Identity;
using SG.Repository.Context;
using SG.WebApi.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SG.WebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.Configuration = config;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            var builder = new ConfigurationBuilder().AddJsonFile("config.json");
            Configuration = builder.Build();
        }
        [HttpGet]
        [Route("getAllUser")]
        [Authorize(Roles = "Admin, Gestor, RH")]
        public async Task<ActionResult<List<UserRole>>> Get([FromServices] SGContext context)
        {

            /*var users = await context.UserRoles
                .Include(u => u.User)
                .Include(r => r.Role)
                .AsNoTracking()
                .ToListAsync();*/

            var users = await context.UserRoles
                .Include(u => u.User)
                .Include(r => r.Role)
                .AsNoTracking()
                .ToListAsync();


            return Ok(users);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var users = await _userManager.FindByEmailAsync(userDto.Email);
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (users == null)
                {
                    
                    var userToReturn = _mapper.Map<UserDto>(user);

                    if (result.Succeeded)
                    {
                        return Created("GetUser", userToReturn);
                    }
                }
                else
                {

                    return BadRequest("Email duplicado");
                    
                }
                return BadRequest(result.Errors);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve uma falha: {ex.Message}");
            }
        }
        [HttpPost("{id}")]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<User>> Put([FromServices] SGContext context, int id, [FromBody] UserDto userDto)
        {
            try
            {
                
                var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
                var passwordResult = _userManager.PasswordHasher.HashPassword(user, userDto.Password);

                if (user == null) return NotFound();

                user.Email = userDto.Email;
                user.UserName = userDto.UserName;                
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.PasswordHash = passwordResult;
                user.FullName = userDto.FullName;
                user.Setor = userDto.Setor;
                user.Funcao = userDto.Funcao;
                var result1 = await _userManager.UpdateAsync(user);

                return Ok();

            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve uma falha: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (result.Succeeded)
                {
                    // var appUser = await _userManager.Users
                    //    .FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());
                    var appUser = await _userManager.Users
                        .FirstOrDefaultAsync(u => u.NormalizedEmail.ToUpper() == userLogin.Email.ToUpper());

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    return Ok(new
                    {
                        token = GenerateJwtToken(appUser).Result,
                        user = userToReturn
                    });
                }

                return Unauthorized();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Houve uma falha: {ex.Message}");
            }
        }
        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //var key1 = Configuration.GetSection("AppSettings:Token").Value;

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.Now.AddDays(1),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
