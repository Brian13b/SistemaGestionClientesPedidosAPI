namespace SistemaGestionClientesPedidos.API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public required string Descripcion { get; set; }
        public DateTime FechaPedido { get; set; }
        public required string Estado { get; set; }
        public int ClienteId { get; set; } // Clave foránea
        public required Cliente Cliente { get; set; } // Relación muchos a uno
    }
}