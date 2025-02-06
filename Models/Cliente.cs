namespace SistemaGestionClientesPedidos.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public required string Telefono { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>(); // Relación uno a muchos
    }
}
