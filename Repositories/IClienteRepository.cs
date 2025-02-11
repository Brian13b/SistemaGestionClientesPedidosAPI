﻿using SistemaGestionClientesPedidos.API.Models;

namespace SistemaGestionClientesPedidos.API.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int id);
        Task AgregarClienteAsync(Cliente cliente);
        Task ActualizarClienteAsync(Cliente cliente);
        Task EliminarClienteAsync(int id);
    }
}
