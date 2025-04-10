using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// Representa un producto en la base de datos.
/// </summary>
public partial class Producto : BaseEntity
{
    /// <summary>
    /// Clave primaria única para el producto.
    /// </summary>
    [Key]
    [Column("id_Producto")]
    public long IdProducto { get; set; }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Precio del producto.
    /// </summary>
    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    /// <summary>
    /// Cantidad en stock del producto.
    /// </summary>
    [Column("stock")]
    public int Stock { get; set; }

    /// <summary>
    /// Marca del producto.
    /// </summary>
    [Column("brand")]
    [StringLength(100)]
    [Unicode(false)]
    public string Brand { get; set; } = null!;

    /// <summary>
    /// Clave foránea que referencia a la categoría del producto.
    /// </summary>
    [Column("id_Categoria")]
    public long IdCategoria { get; set; }

    /// <summary>
    /// Fecha y hora de creación del producto.
    /// </summary>
    [Column("fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Fecha y hora de la última actualización del producto (puede ser nulo).
    /// </summary>
    [Column("fecha_Actualizacion", TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    /// <summary>
    /// Objeto de navegación para la categoría del producto.
    /// </summary>
    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}