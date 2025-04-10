using Infrastructure.AppDbContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementación de la interfaz IRepository para el acceso a datos.
    /// </summary>
    /// <typeparam name="T">El tipo de la entidad.</typeparam>
    public class ProductRepository : Repository<Producto>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Producto>> GetProductsWithInclude()
        {
            return await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .ToListAsync();
        }
        public async Task<List<Producto>> GetProductsByCategoryAsync(long id)
        {
            return await _context.Productos
                .Where(p => p.IdCategoriaNavigation.IdCategoria.Equals(id))
                .ToListAsync();
        }
    }
}
