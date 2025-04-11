using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTO;

namespace SportshopApi.Controllers
{
    /// <summary>
    /// API para la gestión de productos deportivos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductService _productoService;

        /// <summary>
        /// Constructor del ProductosController.
        /// </summary>
        /// <param name="productoService">Servicio para la lógica de negocio de los productos.</param>
        public ProductosController(IProductService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Una lista de todos los productos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllProductsAsync();
            return Ok(productos);
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a obtener.</param>
        /// <returns>El producto con el ID especificado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(long id)
        {
            var producto = await _productoService.GetProductByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        /// <summary>
        /// Obtiene productos por categoría.
        /// </summary>
        /// <param name="categoria">Nombre de la categoría de los productos a obtener.</param>
        /// <returns>Una lista de productos que pertenecen a la categoría especificada.</returns>
        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosPorCategoria(string categoria)
        {
            var productos = await _productoService.GetProductsByCategoryNameAsync(categoria);
            return Ok(productos);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="modeloProducto">Modelo del producto a crear.</param>
        /// <returns>El producto creado.</returns>
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(ProductoDTO modeloProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return await _productoService.CreateProductAsync(modeloProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Devuelve un BadRequest con el mensaje de error del servicio
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a actualizar.</param>
        /// <param name="modeloProducto">Modelo del producto con los datos actualizados.</param>
        /// <returns>Un resultado indicando el estado de la operación.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(long id, ProductoDTO modeloProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productoService.UpdateProductAsync(id, modeloProducto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Un resultado indicando el estado de la operación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(long id)
        {
            try
            {
                await _productoService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al eliminar el producto.");
            }
        }
    }

}
