using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Defines the business operations for managing products.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets a product by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The found product or null if it does not exist.</returns>
        Task<Producto> GetProductByIdAsync(long id);

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        Task<IEnumerable<Producto>> GetAllProductsAsync();

        /// <summary>
        /// Gets the products of a specific category.
        /// </summary>
        /// <param name="categoryName">The name of the category to filter by (e.g., "Clothing", "Footwear", "Accessories").</param>
        /// <returns>A collection of products that belong to the specified category.</returns>
        Task<List<Producto>> GetProductsByCategoryNameAsync(string categoryName);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productModel">The data of the product to create.</param>
        Task<Producto> CreateProductAsync(ProductoDTO productModel);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productModel">The updated data of the product.</param>
        Task UpdateProductAsync(long id, ProductoDTO productModel);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        Task DeleteProductAsync(long id);


    }
}
