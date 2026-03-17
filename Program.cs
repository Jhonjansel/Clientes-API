using Clientes.Shared.Models;
using Clientes.Shared.Repositories;
using Clientes_API.Services;
using Microsoft.Win32;

var builder = WebApplication.CreateBuilder(args); // Se crea el builder que configura la aplicación web

// Registro de servicios en el contenedor de inyección de dependencias.
// Singleton significa que se crea una sola instancia durante toda la vida de la aplicación.
builder.Services.AddSingleton<ClientService>();
builder.Services.AddSingleton<JsonClienteRepository>();


var app = builder.Build(); // Construye la aplicación

// Endpoint GET para obtener todos los clientes
// Ruta: /clientes
app.MapGet("/clientes", (ClientService service) =>
{
    // Llama al servicio para obtener todos los clientes
    return Results.Ok(service.GetAll());
});

// Endpoint GET para obtener un cliente por DNI
// Ruta: /clientes/{dni}
app.MapGet("/clientes/{dni}", (string dni, ClientService service) =>
{
    var cliente = service.GetByDni(dni); // Busca el cliente en el servicio
    // Si no existe, devuelve 404
    if (cliente == null)
        return Results.NotFound();
    // Si existe, devuelve 200 con el cliente
    return Results.Ok(cliente);
});

// Endpoint POST para crear un nuevo cliente
// Ruta: /clientes
app.MapPost("/clientes", (Cliente cliente, ClientService service) =>
{
    // Validación básica: el DNI es obligatorio
    if (string.IsNullOrWhiteSpace(cliente.DNI))
        return Results.BadRequest("DNI obligatorio");

    var created = service.Add(cliente);  // Intenta agregar el cliente usando el servicio
    // Si el DNI ya existe, devuelve error
    if (!created)
        return Results.BadRequest("El DNI ya existe");
    // Si se creó correctamente, devuelve 201 Created
    // incluyendo la URL del nuevo recurso
    return Results.Created($"/clientes/{cliente.DNI}", cliente);
});

// Endpoint DELETE para eliminar un cliente por DNI
// Ruta: /clientes/{dni}
app.MapDelete("/clientes/{dni}", (string dni, ClientService service) =>
{
    // Intenta eliminar el cliente
    var deleted = service.Delete(dni);
    // Si no se encuentra el cliente, devuelve 404
    if (!deleted)
        return Results.NotFound("No encontrado!");
    // Si se eliminó correctamente devuelve 204 (sin contenido)
    return Results.NoContent();
});
// Inicia la aplicación web
app.Run();
