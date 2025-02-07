using Microsoft.AspNetCore.Mvc;
using SistemaGestionClientesPedidos.API.DTOs;
using SistemaGestionClientesPedidos.API.Repositories;
using SistemaGestionClientesPedidos.API.Models;
using Microsoft.AspNetCore.SignalR;
using SistemaGestionClientesPedidos.API.Hubs;

namespace SistemaGestionClientesPedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IHubContext<PedidoHub> _hubContext;

        public PedidosController(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IHubContext<PedidoHub> hubContext)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var pedidos = await _pedidoRepository.ObtenerTodosAsync();

            if (pedidos == null)
            {
                return NotFound();
            }

            var clientes = await _clienteRepository.ObtenerTodosAsync();

            var clientesDict = clientes.ToDictionary(c => c.Id);

            var pedidosResponse = pedidos.Select(p => new PedidoResponseDTO
            {
                Id = p.Id,
                Descripcion = p.Descripcion,
                FechaPedido = p.FechaPedido,
                Estado = p.Estado,
                ClienteId = p.ClienteId,
                Cliente = clientesDict.ContainsKey(p.ClienteId) ? new ClienteResponseDTO
                {
                    Id = p.ClienteId,
                    Nombre = clientesDict[p.ClienteId].Nombre,
                    Email = clientesDict[p.ClienteId].Email,
                    Telefono = clientesDict[p.ClienteId].Telefono
                } : null
            }).ToList();

            return Ok(pedidosResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var cliente = await _clienteRepository.ObtenerPorIdAsync(pedido.ClienteId);

            var pedidoResponse = new PedidoResponseDTO
            {
                Id = pedido.Id,
                Descripcion = pedido.Descripcion,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                ClienteId = pedido.ClienteId,
                Cliente = new ClienteResponseDTO
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono
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

            await _pedidoRepository.AgregarPedidoAsync(pedido);
            await _hubContext.Clients.All.SendAsync("RecibirNotificacion", $"Nuevo pedido creado: {pedido.Descripcion}");
            var cliente = await _clienteRepository.ObtenerPorIdAsync(pedido.ClienteId);

            var pedidoResponse = new PedidoResponseDTO
            {
                Id = pedido.Id,
                Descripcion = pedido.Descripcion,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                ClienteId = pedido.ClienteId,
                Cliente = new ClienteResponseDTO
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono
                }
            };

            return CreatedAtAction(nameof(ObtenerPorId), new { id = pedido.Id }, pedidoResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] PedidoDTO pedidoDTO)
        {
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Descripcion = pedidoDTO.Descripcion;
            pedido.FechaPedido = pedidoDTO.FechaPedido;
            pedido.Estado = pedidoDTO.Estado;
            pedido.ClienteId = pedidoDTO.ClienteId;

            await _pedidoRepository.ActualizarPedidoAsync(pedido);

            var cliente = await _clienteRepository.ObtenerPorIdAsync(pedido.ClienteId);

            var pedidoResponse = new PedidoResponseDTO
            {
                Id = pedido.Id,
                Descripcion = pedido.Descripcion,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                ClienteId = pedido.ClienteId,
                Cliente = new ClienteResponseDTO
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono
                }
            };

            await _hubContext.Clients.All.SendAsync("RecibirNotificacion", $"Pedido Actualizado");

            return Ok(pedidoResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _pedidoRepository.EliminarPedidoAsync(id);
            return NoContent();
        }
    }
}
