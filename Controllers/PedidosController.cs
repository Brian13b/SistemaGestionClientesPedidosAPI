using Microsoft.AspNetCore.Mvc;
using SistemaGestionClientesPedidos.API.DTOs;
using SistemaGestionClientesPedidos.API.Repositories;
using SistemaGestionClientesPedidos.API.Models;

namespace SistemaGestionClientesPedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository pedidoRepository;

        public PedidosController(IPedidoRepository pedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var pedidos = await pedidoRepository.ObtenerTodosAsync();
            var pedidosResponse = pedidos.Select(p => new PedidoResponseDTO
            {
                Id = p.Id,
                Descripcion = p.Descripcion,
                FechaPedido = p.FechaPedido,
                Estado = p.Estado,
                ClienteId = p.ClienteId,
                Cliente = new ClienteResponseDTO
                {
                    Id = p.Cliente.Id,
                    Nombre = p.Cliente.Nombre,
                    Email = p.Cliente.Email,
                    Telefono = p.Cliente.Telefono
                }
            }).ToList();

            return Ok(pedidosResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var pedido = await pedidoRepository.ObtenerPorIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            var pedidoResponse = new PedidoResponseDTO
            {
                Id = pedido.Id,
                Descripcion = pedido.Descripcion,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                ClienteId = pedido.ClienteId,
                Cliente = new ClienteResponseDTO
                {
                    Id = pedido.Cliente.Id,
                    Nombre = pedido.Cliente.Nombre,
                    Email = pedido.Cliente.Email,
                    Telefono = pedido.Cliente.Telefono
                }
            };

            return Ok(pedidoResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PedidoDTO pedidoDTO)
        {
            var pedido = new Pedido
            {
                Descripcion = pedidoDTO.Descripcion,
                FechaPedido = pedidoDTO.FechaPedido,
                Estado = pedidoDTO.Estado,
                ClienteId = pedidoDTO.ClienteId
            };

            await pedidoRepository.AgregarPedidoAsync(pedido);

            var pedidoResponse = new PedidoResponseDTO
            {
                Id = pedido.Id,
                Descripcion = pedido.Descripcion,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                ClienteId = pedido.ClienteId
            };

            return CreatedAtAction(nameof(ObtenerPorId), new { id = pedido.Id }, pedidoResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] PedidoDTO pedidoDTO)
        {
            var pedido = await pedidoRepository.ObtenerPorIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Descripcion = pedidoDTO.Descripcion;
            pedido.FechaPedido = pedidoDTO.FechaPedido;
            pedido.Estado = pedidoDTO.Estado;
            pedido.ClienteId = pedidoDTO.ClienteId;

            await pedidoRepository.ActualizarPedidoAsync(pedido);

            var pedidoResponse = new PedidoResponseDTO
            {
                Id = pedido.Id,
                Descripcion = pedido.Descripcion,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                ClienteId = pedido.ClienteId,
                Cliente = new ClienteResponseDTO
                {
                    Id = pedido.Cliente.Id,
                    Nombre = pedido.Cliente.Nombre,
                    Email = pedido.Cliente.Email,
                    Telefono = pedido.Cliente.Telefono
                }
            };

            return Ok(pedidoResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await pedidoRepository.EliminarPedidoAsync(id);
            return NoContent();
        }
    }
}
