using Business.common;
using Business.Interfaces;
using Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service) 
        { 
            this.service = service; 
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        { 
            if(loginDTO == null || string.IsNullOrEmpty(loginDTO.correo) || string.IsNullOrEmpty(loginDTO.password)) 
                return BadRequest("El correo y la contraseña son obligatorios.");

            var result = await service.Login(loginDTO);

            if (!result.Success) return Unauthorized(new { message = result.Message });

            return Ok(result.Data);

        }
    }
}
