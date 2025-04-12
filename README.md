# SportShopApi


SportShopApi es una API web en .NET 6 nos proporsiona la gestion de los productos de una tienda deportiva.

## Características

- Gestión de Productos: Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre los productos de la tienda.
- Listado de Productos por Categoría: Permite obtener todos los productos pertenecientes a una categoría específica.
- Estadísticas: Proporciona métricas sobre los productos almacenados en la base de datos.

## Endpoints de la API
### Productos
 - **GET /api/Productos**
  - Descripción: Obtiene todos los productos.
  - Respuesta: Array JSON de objetos Producto.
  
Ejemplo:
```json
[
    {
        "idProducto": 1,
        "name": "Camiseta de Fútbol",
        "categoria": "Ropa",
        "price": 25.99,
        "stock": 50,
        "brand": "Nike"
    },
    {
        "idProducto": 2,
        "name": "Zapatillas para Correr",
        "categoria": "Calzado",
        "price": 79.99,
        "stock": 30,
        "brand": "Adidas"
    }
]
```

- **GET /api/Productos/{id}**
 - Descripción: Obtiene un producto específico por su ID.
 - Parámetro de Ruta: id (entero) - El ID del producto a obtener.
 - Respuesta: Objeto JSON del Producto encontrado o código de estado 404 si no existe.

Ejemplo de Respuesta (200 OK):
```json
{
    "idProducto": 1,
    "name": "Camiseta de Fútbol",
    "categoria": "Ropa",
    "price": 25.99,
    "stock": 50,
    "brand": "Nike"
}
```
- **POST /api/Productos**
 - Descripción: Crea un nuevo producto.
 - Request: Objeto JSON del Producto a crear (ver ejemplo).
 - Respuesta: Objeto JSON del Producto creado con su ID asignado.

Ejemplo de Request:
```json
{
    "name": "Balón de Baloncesto",
    "category": "Accesorios",
    "price": 19.99,
    "stock": 20,
    "brand": "Spalding"
}
```
- **PUT /api/Productos/{id}**
 - Descripción: Actualiza un producto existente por su ID.
 - Parámetro de Ruta: id (entero) - El ID del producto a actualizar.
 - Request: Objeto JSON con los datos actualizados del Producto.
 - Respuesta: Código de estado 200 (Ok) si la actualización fue exitosa o 404 si el producto no existe.

Ejemplo de Request:
```json
{
    "name": "Camiseta de Fútbol Edición Limitada",
    "category": "Ropa",
    "price": 39.99,
    "stock": 15,
    "brand": "Nike"
}
```
- **DELETE /api/Productos/{id}**
 - Descripción: Elimina un producto específico por su ID.
 - Parámetro de Ruta: id (entero) - El ID del producto a eliminar.
 - Respuesta: Código de estado 200 (Ok) si la eliminación fue exitosa o 404 si el producto no existe.

### Productos por Categoría
- **GET /api/Productos/categoria/{nombreCategoria}**
 - Descripción: Obtiene todos los productos pertenecientes a una categoría específica.
 - Parámetro de Ruta: nombreCategoria (string) - El nombre de la categoría a filtrar (Ej: "Ropa", "Calzado").
 - Respuesta: Array JSON de objetos Producto que pertenecen a la categoría especificada.

Ejemplo de Respuesta para la categoría "Ropa":
```json
[
    {
        "idProducto": 1,
        "name": "Camiseta de Fútbol",
        "categoria": "Ropa",
        "price": 25.99,
        "stock": 50,
        "brand": "Nike"
    },
    {
        "idProducto": 3,
        "name": "Pantalón Deportivo",
        "categoria": "Ropa",
        "price": 35.50,
        "stock": 40,
        "brand": "Adidas"
    }
]
```

### Estadísticas de Productos
- **GET /api/Metricas/productos**
 - Descripción: Obtiene estadísticas sobre los productos almacenados.
 - Respuesta: Objeto JSON con el conteo total de productos.

Ejemplo:
```json
{
    "total_productos": 150
}
```


## Empezando
### Prerrequisitos
  - .NET 6 SDK
  - Git
    
### Instalación
  - Clona el repositorio: ``https://github.com/ilopezor/SportShopApi.git``
  - Build project: ``dotnet build``
  - Run project: ``dotnet run``
    
### Uso
- Realiza peticiones GET a ``/api/Productos`` para obtener todos los productos.
- Realiza peticiones GET a ``/api/Productos/{id}`` para obtener un producto específico.
- Realiza peticiones POST a ``/api/Productos`` con un objeto JSON para crear un nuevo producto.
- Realiza peticiones PUT a ``/api/Productos/{id}`` con un objeto JSON para actualizar un producto existente.
- Realiza peticiones DELETE a ``/api/Productos/{id}`` para eliminar un producto.
- Realiza peticiones GET a ``/api/Productos/categoria/{nombreCategoria}`` para obtener productos por categoría.
- Realiza peticiones GET a ``/api/Metricas/productos`` para obtener estadísticas de productos.

## Pruebas
- Este proyecto incluye pruebas unitarias utilizando XUnit. Puedes ejecutar las pruebas con:
``dotnet test``

## Documentation
- La documentación de la API está disponible a través de Swagger en la ruta ``/swagger/index.html``

## Arquitectura
- Hexagonal


## Creditos
- Este proyecto fue creado como parte de una prueba de ingreso a la empresa Mercado Libre.

##Estado del Proyecto
- Este proyecto se considera completo y listo para su uso.


## Notas de la Versión
- Se utiliza .NET 6.0 en el proyecto.
- Implementación de funcionalidades CRUD para productos.
- Endpoint para obtener productos por categoría.
- Endpoint para obtener estadísticas básicas de productos.
- Documentación generada con Swagger.
