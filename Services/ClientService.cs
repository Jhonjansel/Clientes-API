using System;
using Clientes.Shared.Models;
using Clientes.Shared.Repositories;

namespace Clientes_API.Services
{
    // Servicio que contiene la lógica de negocio relacionada con los clientes.
    // Se encarga de interactuar con el repositorio para obtener, agregar y eliminar clientes.
    public class ClientService
	{
        private readonly JsonClienteRepository repository; // Repositorio que maneja la persistencia de los clientes en el archivo JSON
        private List<Cliente> clients; // Lista en memoria de los clientes cargados desde el repositorio

        /* 
         * Constructor del servicio.
         * Recibe el repositorio mediante inyección de dependencias
         * y carga todos los clientes existentes al iniciar.
         */
        public ClientService(JsonClienteRepository repo)
        {
            repository = repo;
            clients = repository.GetAll();
        }

        // Devuelve la lista completa de clientes
        public List<Cliente> GetAll()
        {
            return clients;
        }

        /*
         * Busca y devuelve un cliente por su DNI.
         * Si no existe, devuelve null.
         */
        public Cliente GetByDni(string dni)
        {
            return clients.FirstOrDefault(c => c.DNI == dni);
        }

        /* 
         * Agrega un nuevo cliente a la lista.
         * Primero verifica que no exista otro cliente con el mismo DNI.
         * Si existe, retorna false. Si no, lo agrega y guarda los cambios.
         */
        public bool Add(Cliente cliente)
        {
            if (clients.Any(c => c.DNI== cliente.DNI))
                return false;
            clients.Add(cliente);
            repository.SaveAll(clients);  // Guarda la lista actualizada en el archivo JSON
            return true;
        }

        /* 
         * Elimina un cliente según su DNI.
         * Si el cliente no existe, retorna false.
         * Si existe, lo elimina de la lista y guarda los cambios.
        */
        public bool Delete(string dni)
        {
            var cliente = clients.FirstOrDefault(c => c.DNI == dni);
            if (cliente == null)
                return false;
            clients.Remove(cliente);
            repository.SaveAll(clients); // Guarda la lista actualizada después de eliminar el cliente
            return true;
        }
    
	}
}

