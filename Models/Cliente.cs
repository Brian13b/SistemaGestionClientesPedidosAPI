namespace SistemaGestionClientesPedidos.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>(); // Relación uno a muchos
    }
}
