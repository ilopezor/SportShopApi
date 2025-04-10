using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// Implementa la lógica de negocio para obtener métricas de los productos.
    /// </summary>
    public class MetricsService : IMetricsService
    {
        private readonly IRepository<Producto> _productoRepository;
        private readonly IRepository<Categoria> _categoryRepository;

        /// <summary>
        /// Constructor of the MetricasService.
        /// </summary>
        /// <param name="productoRepository">The repository for accessing product data.</param>
        /// <param name="categoriaRepository">The repository for accessing category data.</param>
        public MetricsService(IRepository<Producto> productoRepository, IRepository<Categoria> categoriaRepository)
        {
            _productoRepository = productoRepository;
            _categoryRepository = categoriaRepository;
        }

        /// <inheritdoc />
        public async Task<object> GetProductMetricsAsync()
        {
            try
            {
                var allProducts = await _productoRepository.GetAllAsync();
                var totalProducts = allProducts.Count();
                var totalStock = allProducts.Sum(p => p.Stock);
                var averagePrice = allProducts.Any() ? allProducts.Average(p => p.Price) : 0;

                var categoryCounts = allProducts
                    .GroupBy(p => p.IdCategoria)
                    .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .Take(2);

                var topCategories = await _categoryRepository.GetAllAsync();
                var topCategoryNames = categoryCounts
                    .Join(topCategories,
                          cc => cc.CategoryId,
                          c => c.IdCategoria,
                          (cc, c) => c.CodigoCategoria)
                    .ToList();

                return new
                {
                    total_products = totalProducts,
                    top_categories = topCategoryNames,
                    total_stock = totalStock,
                    average_price = averagePrice
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting product metrics: {ex.Message}");
                return new { error = "Error getting metrics." };
            }
        }
    }
}
