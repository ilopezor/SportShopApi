using Domain.Entities;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.DTO;

namespace Application.Services;
/// <summary>
/// Implementa la lógica de negocio para la gestión de productos.
/// </summary>
public class ProductService : IProductService
{
    private readonly IRepository<Producto> _productRepository;
    private readonly IRepository<Categoria> _categoriaRepository;
    private readonly IProductRepository _productoCategoriaRepository;

    /// <summary>
    /// Constructor del <see cref="ProductService"/>.
    /// </summary>
    /// <param name="productRepository">El repositorio para acceder a los datos de los productos.</param>
    /// <param name="productoCategoriaRepository">El repositorio específico para acceder a datos de productos, incluyendo relaciones.</param>
    /// <param name="categoriaRepository">El repositorio para acceder a los datos de las categorías.</param>
    public ProductService(IRepository<Producto> productRepository, IProductRepository productoCategoriaRepository, IRepository<Categoria> categoriaRepository)
    {
        _productRepository = productRepository;
        _categoriaRepository = categoriaRepository;
        _productoCategoriaRepository = productoCategoriaRepository;
    }


    /// <summary>
    /// Obtiene un producto por su identificador único de forma asíncrona.
    /// </summary>
    /// <param name="id">El identificador único del producto a buscar.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona. El resultado contiene el <see cref="Producto"/> encontrado, o null si no existe.</returns>
    public async Task<Producto> GetProductByIdAsync(long id)
    {
        try
        {
            return await _productRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting product with ID {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Obtiene todos los productos de forma asíncrona.
    /// </summary>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona. El resultado contiene una colección de todos los <see cref="Producto"/>.</returns>
    public async Task<IEnumerable<Producto>> GetAllProductsAsync()
    {
        try
        {
            return await _productRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all products: {ex.Message}");
            return new List<Producto>();
        }
    }

    /// <summary>
    /// Obtiene una lista de productos por el nombre de su categoría de forma asíncrona.
    /// </summary>
    /// <param name="categoryName">El nombre de la categoría por la cual filtrar los productos.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona. El resultado contiene una lista de <see cref="Producto"/> que pertenecen a la categoría especificada.</returns>
    public async Task<List<Producto>> GetProductsByCategoryNameAsync(string categoryName)
    {
        try
        {
            List<Producto> productEntity = await _productoCategoriaRepository.GetProductsWithInclude();

            if (!productEntity.Any())
            {
                return new List<Producto>();
            }

            List<Producto> filteredProducts = productEntity
                .Where(p => p.IdCategoriaNavigation?.CodigoCategoria?.ToLower() == categoryName.ToLower())
                .ToList();

            return filteredProducts;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting products by category '{categoryName}': {ex.Message}");
            return new List<Producto>();
        }
    }

    /// <summary>
    /// Crea un nuevo producto de forma asíncrona.
    /// </summary>
    /// <param name="productModel">El modelo de producto (<see cref="ProductoDTO"/>) con la información del nuevo producto.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona. El resultado contiene el <see cref="Producto"/> creado.</returns>
    /// <exception cref="Exception">Se lanza una excepción si la categoría especificada en el modelo no existe.</exception>
    public async Task<Producto> CreateProductAsync(ProductoDTO productModel)
    {
        try
        {
            var category = await GetCategoryByNameAsync(productModel.Category);

            if (category is null)
            {
                category = await _categoriaRepository.AddAsync(new Categoria
                {
                    CodigoCategoria = productModel.Category,
                    DescripcionCategoria = productModel.Category,
                });    
            }

            var productEntity = new Producto
            {
                Name = productModel.Name,
                IdCategoria = category.IdCategoria,
                Price = productModel.Price,
                Stock = productModel.Stock,
                Brand = productModel.Brand,
                FechaCreacion = DateTime.Now
            };

            await _productRepository.AddAsync(productEntity);
            return productEntity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating product '{productModel.Name}': {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Actualiza un producto existente de forma asíncrona.
    /// </summary>
    /// <param name="id">El identificador único del producto a actualizar.</param>
    /// <param name="productModel">El modelo de producto (<see cref="ProductoDTO"/>) con la información actualizada.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona.</returns>
    /// <exception cref="Exception">Se lanza una excepción si el producto con el ID especificado no se encuentra o si la categoría especificada en el modelo no existe.</exception>
    public async Task UpdateProductAsync(long id, ProductoDTO productModel)
    {
        try
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            var category = await GetCategoryByNameAsync(productModel.Category);
            if (category is null)
            {
                category = await _categoriaRepository.AddAsync(new Categoria
                {
                    CodigoCategoria = productModel.Category,
                    DescripcionCategoria = productModel.Category,
                });
            }

            existingProduct.Name = productModel.Name;
            existingProduct.IdCategoria = category.IdCategoria;
            existingProduct.Price = productModel.Price;
            existingProduct.Stock = productModel.Stock;
            existingProduct.Brand = productModel.Brand;
            existingProduct.FechaActualizacion = DateTime.Now;

            await _productRepository.UpdateAsync(existingProduct);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product with ID {id}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Elimina un producto por su identificador único de forma asíncrona.
    /// </summary>
    /// <param name="id">El identificador único del producto a eliminar.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona.</returns>
    /// <exception cref="Exception">Se lanza una excepción si ocurre un error durante la eliminación.</exception>
    public async Task DeleteProductAsync(long id)
    {
        try
        {
            await _productRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product with ID {id}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Obtiene una categoría por su nombre de forma asíncrona (método privado interno).
    /// </summary>
    /// <param name="categoryName">El nombre de la categoría a buscar.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona. El resultado contiene la <see cref="Categoria"/> encontrada, o null si no existe.</returns>
    private async Task<Categoria?> GetCategoryByNameAsync(string categoryName)
    {
        var categories = await _categoriaRepository.GetAllAsync();
        return categories.FirstOrDefault(c => c.CodigoCategoria.ToLower() == categoryName.ToLower());
    }

    /// <summary>
    /// Obtiene un producto por su identificador único de forma asíncrona (método privado interno).
    /// </summary>
    /// <param name="id">El identificador único del producto a buscar.</param>
    /// <returns>Un <see cref="Task"/> que representa la operación asíncrona. El resultado contiene el <see cref="Producto"/> encontrado.</returns>
    private async Task<Producto> GetProductsByCategoryAsync(long id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

}