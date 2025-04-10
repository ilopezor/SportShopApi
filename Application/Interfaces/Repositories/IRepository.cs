using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Define las operaciones básicas de acceso a datos para una entidad.
    /// </summary>
    /// <typeparam name="T">El tipo de la entidad.</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Obtiene una entidad por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a obtener.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        Task<T> GetByIdAsync(long id);

        /// <summary>
        /// Obtiene todas las entidades.
        /// </summary>
        /// <returns>Una colección de todas las entidades.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Agrega una nueva entidad.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">La entidad con los datos actualizados.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Elimina una entidad por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a eliminar.</param>
        Task DeleteAsync(long id);
    }
}
