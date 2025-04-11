using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    /// <summary>
    /// Representa un producto deportivo en la tienda.
    /// </summary>
    public class ProductoDTO
    {
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Name { get; set; }

        /// <summary>
        /// Categoría del producto (Ej: "Ropa", "Calzado", "Accesorios").
        /// </summary>
        [Required(ErrorMessage = "La categoría es requerida.")]
        public string Category { get; set; }

        /// <summary>
        /// Precio del producto.
        /// </summary>
        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Cantidad de este producto en stock.
        /// </summary>
        [Required(ErrorMessage = "El stock es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        /// <summary>
        /// Marca del producto.
        /// </summary>
        [Required(ErrorMessage = "La marca es requerida.")]
        public string Brand { get; set; }
    }
}
