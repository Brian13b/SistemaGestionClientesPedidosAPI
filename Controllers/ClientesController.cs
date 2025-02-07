using Microsoft.AspNetCore.Mvc;
using SistemaGestionClientesPedidos.API.DTOs;
using SistemaGestionClientesPedidos.API.Repositories;
using SistemaGestionClientesPedidos.API.Models;

namespace SistemaGestionClientesPedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _clienteRepository.ObtenerTodosAsync();
            var clientesResponse = clientes.Select(c => new ClienteResponseDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
                Telefono = c.Telefono
            }).ToList();

            return Ok(clientesResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        { 
            var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            var clienteResponse = new ClienteResponseDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return Ok(clienteResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Email = clienteDTO.Email,
                Telefono = clienteDTO.Telefono
            };

            await _clienteRepository.AgregarClienteAsync(cliente);

            var clienteResponse = new ClienteResponseDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return CreatedAtAction(nameof(ObtenerPorId), new { id = cliente.Id }, clienteResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Nombre = clienteDTO.Nombre;
            cliente.Email = clienteDTO.Email;
            cliente.Telefono = clienteDTO.Telefono;

            await _clienteRepository.ActualizarClienteAsync(cliente);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _clienteRepository.EliminarClienteAsync(id);

            return NoContent();
        }
    }
}
