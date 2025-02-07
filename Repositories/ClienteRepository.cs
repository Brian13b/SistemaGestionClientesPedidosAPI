using Microsoft.EntityFrameworkCore;
using SistemaGestionClientesPedidos.API.Data;
using SistemaGestionClientesPedidos.API.Models;

namespace SistemaGestionClientesPedidos.API.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
