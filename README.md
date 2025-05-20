En el archivo AppSettings.json configurar la cadena de conexión con el servidor correspondiente, cambiar [Nuevo Valor] por los valores correctos 

 "ConnectionStrings": {
   "DefaultConnection": "Server=[Nuevo valor];Database=AMoralesCRUDUsuarios;TrustServerCertificate=True;User ID=[Nuevo Valor];Password=[Nuevo Valor];"
 },

Abrir la consola de nugget y ejecutar los comandos 
1. Add-Migration <NombreMigracion>
2. Update-Database


/Api/Usuario/Add
Ejecutar el proyecto y registrarse con este JSON
{
  "userName": "",
  "nombre": "",
  "apellidoPaterno": "",
  "apellidoMaterno": "",
  "fechaNacimiento": "37/1/93",
  "sexo": "",
  "telefono": "",
  "celular": "",
  "estatus": true,
  "curp": "",
  "login": {
    "email": "",
    "password": ""
  }
}

Al momento de agregar, la información será validada con DataAnnotations por lo que si no cumple con algún REGEX se hará saber en el JSON de respuesta

/Api/Login/login

Para loggearse se necesita enviar este JSON
{
  "email": "",
  "password": ""
}

De respuesta dará el Token que se tiene que ocupar para todas las peticiones: 

/Api/Usuario/Add

/Api/Usuario/GetAll

/Api/Usuario/GetById/{id}

/Api/Usuario/Update

/Api/Usuario/Delete/{id}




