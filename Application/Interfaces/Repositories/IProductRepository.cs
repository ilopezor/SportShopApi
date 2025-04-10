using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Producto>
    {
        Task<List<Producto>> GetProductsWithInclude();

        Task<List<Producto>> GetProductsByCategoryAsync(long categoriaId);
    }
}
