using CCL.DTOs;
using CCL.Entities;
using CCL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCL.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductRepository _repository;
        //private readonly IMapper _mapper;

        public ProductosController(IProductRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }

        [HttpGet("inventario")]
        public  ActionResult<IEnumerable<Producto>> GetInventario()
        {
            var productos = _repository.ObtenerInventario();
            return Ok(productos);
        }

        [HttpPost("movimiento")]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] MovimientoDTO request)
        {
            if (request.Tipo != "entrada" && request.Tipo != "salida")
                return BadRequest("Tipo de movimiento inválido. Use 'entrada' o 'salida'.");

            var producto = await _repository.ObtenerPorId(request.IdProducto);
            if (producto == null)
                return NotFound("Producto no encontrado");

            var nuevaCantidad = request.Tipo == "entrada"
                ? producto.Cantidadproducto + request.Cantidad
                : producto.Cantidadproducto - request.Cantidad;

            if (nuevaCantidad < 0)
                return BadRequest("No hay suficiente stock para realizar la salida");

            await _repository.ActualizarCantidad(request.IdProducto, nuevaCantidad);
            return Ok();
        }
    }
}
