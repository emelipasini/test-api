# Test API

### WebAPI

    - Controllers es lo que maneja las peticiones de la API
    - Connection.cs es el archivo de configuracion de la base de datos
    - Program.cs es el archivo con el que arranca la aplicacion
    - Startup.cs contiene toda la configuracion de la aplicacion
    - Mapper es el mapeo de la base de datos, lo que permite trabajar con la misma al definir como se llama cada tabla con cada una de sus columnas ya que ASP.NET y Postgre no trabajan con los mismas convenciones
    - Models define los modelos de los datos con los que trabaja la aplicacion y se guardan en la base de datos
    - Services contiene las implementaciones de los controladores

### Tests

    - Fixtures contiene los archivos de mock de cada entidad
    - Helpers contiene la logica que se comparte entre entidades
    - System es el testeo de la aplicacion
        - Controllers testeo de los controladores
	    - Services testeo de los servicios

### Entidades

    - Usuarios salio de un tutorial para guiarme a hacer los tests de los controladores que golpean un endpoint
    - Manzanas es mi implementacion de tests de controladores que golpean un endpoint
    - Calles es un controlador con un CRUD a una base de datos en postgre. Tanto controlador como servicio estan testeados.
    - Auth es una implementacion de prueba de como manejar json web token

### Funcionamiento

Para que la aplicacion funcione hay que completar el archivo de appsettings y crear un archivo testsettings.json en la raiz de Tests con la misma informacion.

