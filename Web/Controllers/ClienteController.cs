using Business.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService service;

        public ClienteController(IClienteService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await service.GetAll()).Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);

            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> create([FromBody] Cliente cliente)
        {
            if (cliente == null) return BadRequest("Los datos son incorrectos");

            var result = await service.Create(cliente);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest("El id del cliente no coincide con el de la URL");

            var result = await service.Update(cliente);

            if (!result.Success) return NotFound(result.Message);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id) 
        {
            var result = await service.Delete(id);

            if (!result.Success) return NotFound(result.Message);

            return NoContent();
        }
    }
}