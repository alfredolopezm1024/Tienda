using Business.Interfaces;
using Entity.DTO;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] OrderRequestDTO orderRequestDTO) 
        {
            var result = await service.create(orderRequestDTO);

            if (!result.Success) return StatusCode(500, result.Message);

            return StatusCode(201, "Exito");
        }
    }
}
