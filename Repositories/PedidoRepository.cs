using Microsoft.EntityFrameworkCore;
using SistemaGestionClientesPedidos.API.Data;
using SistemaGestionClientesPedidos.API.Models;

namespace SistemaGestionClientesPedidos.API.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPedidoAsync(int id)
        {
            Pedido pedido = await ObtenerPorIdAsync(id);

            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Pedido> ObtenerPorIdAsync(int id)
        {
            return await _context.Pedidos
                .FindAsync(id);
        }

        public async Task<List<Pedido>> ObtenerTodosAsync()
        {
            return await _context.Pedidos
                .ToListAsync();
        }
    }
}
