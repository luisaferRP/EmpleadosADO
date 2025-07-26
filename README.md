# Sistema de Gestión de Usuarios (CRUD)

Este proyecto es una solución integral desarrollada en **.NET Framework** que implementa una arquitectura de dos capas (API RESTful y Cliente Web MVC) para la gestión de usuarios (empleados), siguiendo los principios de las operaciones CRUD (Crear, Leer, Actualizar, Eliminar).

Fue desarrollado como parte de una prueba técnica para el rol de Desarrollador C#.

## 🚀 Funcionalidades Principales

El sistema permite las siguientes operaciones sobre los datos de usuarios:

* **Listar Usuarios:** Visualización de todos los usuarios registrados en el sistema.
* **Agregar Usuario:** Formulario para la creación de nuevos registros de usuario.
* **Actualizar Usuario:** Edición de la información de usuarios existentes.
* **Eliminar Usuario:** Proceso para eliminar un usuarios del sistema.

## 🛠️ Tecnologías Utilizadas

### Backend (API RESTful)
* **.NET Framework:** Versión 4.7.2 (o similar, según tu configuración).
* **ASP.NET Web API 2:** Para construir la API RESTful.
* **ADO.NET:** Para la interacción con la base de datos SQL Server.
* **Unity Framework:** Como Contenedor de Inversión de Control (IoC) y para Inyección de Dependencias.
    * `Unity` (v5.11.10)
    * `Unity.WebAPI` (v5.4.0)

### Frontend (Cliente Web)
* **.NET Framework:** Versión 4.7.2 (o similar).
* **ASP.NET MVC 5:** Para la interfaz de usuario web.
* **Unity Framework:** Para Inyección de Dependencias en el lado de la presentación.
    * `Unity.Mvc` o `Unity.AspNet.Mvc` (si usas uno de estos en el frontend)
    * `Unity` (v5.11.10)
* **HTML5 / CSS3:** Estructura y estilos de la interfaz.
* **Bootstrap:** Framework CSS para un diseño responsivo y moderno.

### Base de Datos
* **SQL Server:** Sistema de gestión de bases de datos relacionales.

## 📁 Estructura del Proyecto

La solución se compone de dos proyectos principales:

1.  **`PruebaTecnicaEmpleados.Api`**:
    * Proyecto ASP.NET Web API.
    * Contiene los controladores API para las operaciones CRUD de usuarios.
    * Implementa la lógica de acceso a datos (ADO.NET).
    * Configuración de Unity para la inyección de dependencias a nivel de API.

2.  **`PruebaTecnicaEmpleados.Presentation`**:
    * Proyecto ASP.NET MVC.
    * Consume la API RESTful anterior para mostrar y gestionar los usuarios.
    * Contiene las vistas (`.cshtml`) para listar, crear, editar, ver detalles y eliminar usuarios.
      
3.  **`PruebaTecnicaEmpleados.ConsoleApp`**:
     * Contiene la resolucion de ejercicios puestos.
    
```
.
├── EmpleadosADO.sln                     # Archivo de la solución de Visual Studio
├── PruebaTecnicaEmpleados.Api           # Proyecto ASP.NET Web API
│   ├── App_Start                        # Configuración de rutas, WebApiConfig, etc.
│   ├── Controllers                      # Controladores de la API (ej. UsersController.cs)
│   ├── Models                           # Modelos de datos para la API (ej. User.cs)
│   ├── Services                         # Capa de servicio/lógica de negocio
│   │   └── ApiService.cs                # Lógica para interactuar con la DB o repositorios
│   ├── Repositories                     # Capa de acceso a datos (ADO.NET con LINQ)
│   │   └── UserRepository.cs
│   ├── Web.config                       # Configuración del proyecto API (cadenas de conexión, appSettings)
│   └── ...                              # Otros archivos del proyecto API
│
├── PruebaTecnicaEmpleados.Presentation  # Proyecto ASP.NET MVC (Frontend Web)
│   ├── App_Start                        # Configuración de rutas, Unity, etc.
│   ├── Controllers                      # Controladores MVC (ej. UsersController.cs)
│   ├── Models                           # Modelos de vista (ej. User.cs con Data Annotations)
│   │   └── Validation                   # Carpeta para atributos de validación personalizados
│   │       └── PastDateAttribute.cs
│   ├── Views                            # Vistas Razor (.cshtml)
│   │   ├── Shared                       # Vistas compartidas (_Layout.cshtml, _ValidationScriptsPartial.cshtml)
│   │   └── Users                        # Vistas específicas del controlador Users
│   │       ├── Index.cshtml
│   │       ├── Create.cshtml
│   │       ├── Edit.cshtml
│   │       ├── Details.cshtml
│   │       └── Delete.cshtml
│   ├── Content                          # Archivos CSS, imágenes
│   ├── Scripts                          # Archivos JavaScript (jQuery, Bootstrap JS)
│   ├── Web.config                       # Configuración del proyecto web (ApiBaseUrl)
│   └── ...                              # Otros archivos del proyecto MVC
│
└── PruebaTecnicaEmpleados.LogicProblems # Nuevo Proyecto de Consola para ejercicios de lógica
    ├── Program.cs                       # Archivo principal con las soluciones de los ejercicios
    └── ...                              # Otros archivos si se crean clases auxiliares
```

## 🌐 Endpoints de la API

La API expone los siguientes endpoints para la gestión de usuarios/empleados. Se recomienda el uso de herramientas como Postman o Swagger para probar estos endpoints, El proyecto .api esta documentado con swagger y se abre por defecto.

### 1. Obtener Listado de Usuarios

* **Descripción:** Permite recuperar un listado de todos los usuarios registrados en el sistema.
* **Método HTTP:** `GET`
* **Ruta:** `/api/Users`
* **Parámetros de Consulta:** Ninguno.
* **Respuestas:**
    * `200 OK`: Retorna una lista de objetos `User`.
        ```json
        [
            {
                "Id": 1,
                "Name": "Juan",
                "LastName": "Perez",
                "Address": "Calle 123",
                "Phone": "1234567",
                "Birthdate": "1990-05-15T00:00:00",
                "Dni": "123456789",
                "Email": "juan.perez@example.com"
            },
        ]
        ```
    * `204 No Content`: Si no hay usuarios registrados en el sistema.

### 2. Obtener Usuario por ID

* **Descripción:** Permite recuperar los detalles de un usuario específico utilizando su identificador único.
* **Método HTTP:** `GET`
* **Ruta:** `/api/Users/{id}`
* **Parámetros de Ruta:**
    * `id` (entero): El ID único del usuario a recuperar.
* **Respuestas:**
    * `200 OK`: Retorna el objeto `User` correspondiente al ID proporcionado.
        ```json
        {
            "Id": 1,
            "Name": "Juan",
            "LastName": "Perez",
            "Address": "Calle 123",
            "Phone": "1234567",
            "Birthdate": "1990-05-15T00:00:00",
            "Dni": "123456789",
            "Email": "juan.perez@example.com"
        }
        ```
    * `400 Bad Request`: Si el `id` proporcionado no es un formato válido.
    * `404 Not Found`: Si no se encuentra ningún usuario con el `id` especificado.

### 3. Crear Nuevo Usuario

* **Descripción:** Permite registrar un nuevo usuario en el sistema. Incluye validaciones del lado del servidor.
* **Método HTTP:** `POST`
* **Ruta:** `/api/Users`
* **Request Body (JSON):** Se espera un objeto `User` con los datos del nuevo usuario.
    ```json
    {
        "Name": "Carlos",
        "LastName": "Ramirez",
        "Address": "Carrera 45 #6-78",
        "Phone": "5551234",
        "Birthdate": "1992-03-22",
        "Dni": "102030405",
        "Email": "carlos.r@example.com"
    }
    ```
    *Nota: El campo `Id` no debe ser incluido en el Request Body para la creación, ya que es autoincremental.*
* **Respuestas:**
    * `201 Created`: El usuario fue creado exitosamente.
    * `400 Bad Request`: Si los datos proporcionados son inválidos (ej. campos requeridos vacíos, fecha de nacimiento futura, cédula duplicada, formato de email inválido). Los detalles de los errores se incluirán en el cuerpo de la respuesta.

### 4. Actualizar Usuario Existente

* **Descripción:** Permite modificar la información de un usuario existente.
* **Método HTTP:** `PUT`
* **Ruta:** `/api/Users/{id}`
* **Parámetros de Ruta:**
    * `id` (entero): El ID único del usuario a actualizar.
* **Request Body (JSON):** Se espera un objeto `User` con los datos actualizados. El `Id` en el Request Body debe coincidir con el `id` en la ruta.
    ```json
    {
        "Id": 1,
        "Name": "Juan",
        "LastName": "Perez Actualizado",
        "Address": "Nueva Calle 123",
        "Phone": "1234567",
        "Birthdate": "1990-05-15",
        "Dni": "123456789",
        "Email": "juan.perez.updated@example.com"
    }
    ```
* **Respuestas:**
    * `204 No Content`: El usuario fue actualizado exitosamente. (Comúnmente se retorna 204 para PUTs exitosos sin contenido en la respuesta).
    * `400 Bad Request`: Si los datos proporcionados son inválidos o si el `id` en la ruta no coincide con el `Id` en el cuerpo de la solicitud, o si hay violaciones de validación (ej. cédula duplicada por otro usuario).
    * `404 Not Found`: Si no se encuentra ningún usuario con el `id` especificado para actualizar.

### 5. Eliminar Usuario

* **Descripción:** Permite eliminar un usuario del sistema utilizando su identificador único.
* **Método HTTP:** `DELETE`
* **Ruta:** `/api/Users/{id}`
* **Parámetros de Ruta:**
    * `id` (entero): El ID único del usuario a eliminar.
* **Respuestas:**
    * `204 No Content`: El usuario fue eliminado exitosamente.
    * `400 Bad Request`: Si el `id` proporcionado no es un formato válido.
    * `404 Not Found`: Si no se encuentra ningún usuario con el `id` especificado para eliminar.

      
## ⚙️ Configuración y Ejecución

### Prerrequisitos

* **Visual Studio:** Versión 2017 o superior.
* **.NET Framework 4.7.2 (o superior):** Asegúrate de tener los paquetes de desarrollo instalados.
* **SQL Server:** Una instancia de SQL Server donde puedas crear una base de datos.

### 1. Configuración de la Base de Datos

* **Crear la Base de Datos:** Abre SQL Server Management Studio (SSMS) y crea una nueva base de datos. Puedes llamarla `EmpleadosDB` como lo hice yo o el nombre que prefieras.
* **Crear la Tabla de Usuarios:** Ejecuta el siguiente script SQL para crear la tabla `Users` en tu base de datos recién creada:

    ```sql
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Address NVARCHAR(255),
        Phone NVARCHAR(20),
        Birthdate DATE NOT NULL,
        Dni NVARCHAR(20) UNIQUE NOT NULL,
        Email NVARCHAR(100)
    );
    ```

### 3. Abrir y Ejecutar la Solución

1.  **Clona el repositorio:**
    ```bash
    git clone [https://github.com/luisaferRP/EmpleadosADO.git](https://github.com/luisaferRP/EmpleadosADO.git)
    cd EmpleadosADO
    ```
2.  **Abre la Solución:** Abre el archivo `EmpleadosADO.sln` en Visual Studio.
3.  * **Configurar la Cadena de Conexión:**
    * Abre el archivo `Web.config` dentro del proyecto `PruebaTecnicaEmpleados.Api`.
    * Busca la sección `<connectionStrings>` y actualiza la cadena de conexión para que apunte a tu base de datos SQL Server:

        ```xml
        <connectionStrings>
            <add name="DefaultConnection" 
                 connectionString="Data Source=localhost\SQLEXPRESSL;Initial Catalog=TU_NOMBRE_BASE_DE_DATOS;Integrated Security=True;MultipleActiveResultSets=True;" 
                 providerName="System.Data.SqlClient" />
        </connectionStrings>
        ```
        Reemplaza `localhost\SQLEXPRESS` (ej. `(localdb)\MSSQLLocalDB`, `localhost\SQLEXPRESS`) y `TU_NOMBRE_BASE_DE_DATOS` (ej. `EmpleadosDB`).
      
4.   **Limpiar y Recompilar:**
    * En el "Explorador de Soluciones", haz clic derecho en el nodo superior de tu **Solución**.
    * Selecciona "Limpiar solución" (Clean Solution).
    * Luego, selecciona "Recompilar solución" (Rebuild Solution).
     
5. **Configurar Proyectos de Inicio (para Ejecutar):**
    Puedes elegir entre dos modos de ejecución:

    #### a) Ejecutar la Aplicación Web Completa (API + Frontend MVC)
    Esta opción inicia tanto la API como la interfaz web, permitiéndote interactuar con el sistema CRUD de usuarios.

    * En el "Explorador de Soluciones", haz clic derecho en el nodo superior de la **Solución**.
    * Selecciona **"Establecer proyectos de inicio..." (Set Startup Projects...)**.
    * Elige la opción **"Múltiples proyectos de inicio" (Multiple startup projects)**.
    * Para `PruebaTecnicaEmpleados.Api`, asegúrate de que la "Acción" (Action) esté configurada como **"Iniciar" (Start)**.
    * Para `PruebaTecnicaEmpleados.Presentation`, asegúrate de que la "Acción" (Action) también esté configurada como **"Iniciar" (Start)**.
    * Para `PruebaTecnicaEmpleados.ConsoleApp` (el proyecto de consola), la acción debe ser **"Ninguno" (None)** o "Omitir" (Skip) para que no se inicie con la web.
    * Haz clic en "Aceptar".
    * Ahora, presiona `F5` o haz clic en el botón "Iniciar" en Visual Studio. Esto iniciará ambos proyectos (API y Cliente Web).

    #### b) Ejecutar Solo los Ejercicios de Lógica (Aplicación de Consola)
    Esta opción inicia únicamente la aplicación de consola que contiene las soluciones a los ejercicios de lógica, mostrando su salida directamente en una ventana de consola.

    * En el "Explorador de Soluciones", haz clic derecho directamente en el proyecto **`PruebaTecnicaEmpleados.LogicProblems`**.
    * Selecciona **"Establecer como proyecto de inicio" (Set as StartUp Project)**.
    * Presiona `F5` o `Ctrl + F5`. Esto iniciará solo la ventana de la consola con la salida de tus ejercicios.

6.  **Limpiar y Recompilar:**
    * Haz clic derecho en la **Solución** en el "Explorador de Soluciones".
    * Selecciona "Limpiar solución" (Clean Solution).
    * Luego, selecciona "Recompilar solución" (Rebuild Solution).
7.  **Ejecutar:** Presiona `F5` o haz clic en el botón "Iniciar" en Visual Studio. Esto iniciará ambos proyectos (API y Cliente Web).

---

## Contacto

Si tienes alguna pregunta o sugerencia sobre el proyecto, no dudes en contactar a:

- **Desarrollador**: Luisa Fernanda Ramírez Porras
- **Email**: luisaramiresporras103@gmail.com
- **Proyecto**: Prueba Técnica
- **Fecha**: Julio 26 del 2025

## Licencia

Este proyecto fue desarrollado como parte de una prueba técnica ❤️ Cada día aprendemos algo nuevo 
---
