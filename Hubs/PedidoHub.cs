using Microsoft.AspNetCore.SignalR;

namespace SistemaGestionClientesPedidos.API.Hubs
{
    public class PedidoHub : Hub
    {
        // Metodos para enviar notificaciones a los clientes. Ejemplo:
        //
        // public async Task EnviarNotificacion(string mensaje)
        // {
        //     await Clients.All.SendAsync("RecibirNotificacion", mensaje);
        // }
    }
}