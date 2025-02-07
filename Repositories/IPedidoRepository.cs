using SistemaGestionClientesPedidos.API.Models;

namespace SistemaGestionClientesPedidos.API.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> ObtenerPorIdAsync(int id);
        Task<List<Pedido>> ObtenerTodosAsync();
        Task AgregarPedidoAsync(Pedido pedido);
        Task ActualizarPedidoAsync(Pedido pedido);
        Task EliminarPedidoAsync(int id);
    }
}
