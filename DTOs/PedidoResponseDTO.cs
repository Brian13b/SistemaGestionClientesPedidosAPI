namespace SistemaGestionClientesPedidos.API.DTOs
{
    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public int ClienteId { get; set; }
        public ClienteResponseDTO Cliente { get; set; }
    }
}
