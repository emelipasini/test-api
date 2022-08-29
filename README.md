# Test API

### WebAPI

Es la API en si, que tiene como dependencias a Mapper y Models para poder funcionar

    - Controllers es lo que maneja las peticiones de la API
    - Connection.cs es el archivo de configuracion de la base de datos
    - Program.cs es el archivo con el que arranca la aplicacion
    - Startup.cs contiene toda la configuracion de la aplicacion

### Mapper

Es el mapeo de la base de datos, lo que permite trabajar con la misma al definir como se llama cada tabla con cada una de sus columnas ya que ASP.NET y Postgre no trabajan con los mismas convenciones.

### Models

Define los modelos de los datos con los que trabaja la aplicacion y se guardan en la base de datos

