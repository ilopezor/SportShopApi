using Domain.Entities;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;

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
    /// Constructor del ProductService.
    /// </summary>
    /// <param name="productRepository">El repositorio para acceder a los datos de los productos.</param>
    /// <param name="categoriaRepository">El repositorio para acceder a los datos de las categorías.</param>
    public ProductService(IRepository<Producto> productRepository, IProductRepository productoCategoriaRepository, IRepository<Categoria> categoriaRepository)
    {
        _productRepository = productRepository;
        _categoriaRepository = categoriaRepository;
        _productoCategoriaRepository = productoCategoriaRepository;
    }


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

    /// <inheritdoc />
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

    /// <inheritdoc />
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
                .Where(p => p.IdCategoriaNavigation.CodigoCategoria.ToLower() == categoryName.ToLower())
                .ToList();

            return filteredProducts;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting products by category '{categoryName}': {ex.Message}");
            return new List<Producto>();
        }
    }

    /// <inheritdoc />
    public async Task<Producto> CreateProductAsync(ProductoBM productModel)
    {
        try
        {
            var category = await GetCategoryByNameAsync(productModel.Category);

            if (category == null)
            {
                throw new Exception($"The category '{productModel.Category}' does not exist.");
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

    /// <inheritdoc />
    public async Task UpdateProductAsync(long id, ProductoBM productModel)
    {
        try
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            var category = await GetCategoryByNameAsync(productModel.Category);
            if (category == null)
            {
                throw new Exception($"The category '{productModel.Category}' does not exist.");
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

    /// <inheritdoc />
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

    private async Task<Categoria?> GetCategoryByNameAsync(string categoryName)
    {
        var categories = await _categoriaRepository.GetAllAsync();
        return categories.FirstOrDefault(c => c.CodigoCategoria.ToLower() == categoryName.ToLower());
    }

    private async Task<Producto> GetProductsByCategoryAsync(long id)
    {
        return await _productRepository.GetByIdAsync(id);
    }
}


