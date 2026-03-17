# Libreria Compartida DLL
Contiene código reutilizable entre DESKTOP y API: 
https://github.com/Jhonjansel/Clientes-SHARED

# Clientes-API
Endpoints

La API tiene estos endpoints:

# GET todos los clientes
# GET /clientes
Respuesta:

[
  {
    "dni": "12345678A",
    "nombre": "Jhonjansel",
    "apellidos": "Hilario",
    "telefono": "600123456",
    "email": "jhonjansel@gmail.com"
  }
]

# GET cliente por DNI
# GET /clientes/12345678A
Respuesta:
200 OK
o
404 NotFound

# POST crear cliente
# POST /clientes
Body:
{
  "dni": "22222222B",
  "nombre": "Maria",
  "apellidos": "Torres",
  "telefono": "600222222",
  "email": "maria@gmail.com"
}
Respuesta:
201 Created
o
400 BadRequest

# DELETE cliente
# DELETE /clientes/12345678A
Respuesta:
204 NoContent
o
404 NotFound
