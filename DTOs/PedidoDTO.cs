namespace SistemaGestionClientesPedidos.API.DTOs
{
    public class PedidoDTO
    {
        public string Descripcion { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public int ClienteId { get; set; }
    }
}
