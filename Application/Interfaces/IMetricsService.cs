namespace Application.Interfaces
{
    public interface IMetricsService
    {
        /// <summary>
        /// Gets the metrics of the registered products.
        /// </summary>
        /// <returns>An anonymous object with the total number of products, main categories, total stock, and average price.</returns>
        Task<object> GetProductMetricsAsync();
    }
}
