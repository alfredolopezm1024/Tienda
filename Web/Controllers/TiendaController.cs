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
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaService service;

        public TiendaController(ITiendaService service)
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

        [HttpGet("{id}/articulos")]
        public async Task<IActionResult> GetArticulosById(int id) 
        {
            var result = await service.GetArticulosByTiendaId(id);

            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] Tienda tienda)
        {
            if (tienda == null) return BadRequest("Los datos son incorrectos");

            var result = await service.Create(tienda);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Tienda tienda)
        {
            if (id != tienda.Id) return BadRequest("El id de la tienda no coincide con el de la URL");

            var result = await service.Update(tienda);

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
