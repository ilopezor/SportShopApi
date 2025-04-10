using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;

/// <summary>
/// Representa una categoría de productos en la base de datos.
/// </summary>
public partial class Categoria : BaseEntity
{
    /// <summary>
    /// Clave primaria única para la categoría.
    /// </summary>
    [Key]
    [Column("id_Categoria")]
    public long IdCategoria { get; set; }

    /// <summary>
    /// Código único para la categoría.
    /// </summary>
    [Column("codigo_Categoria")]
    [StringLength(50)]
    [Unicode(false)]
    public string CodigoCategoria { get; set; } = null!;

    /// <summary>
    /// Descripción de la categoría.
    /// </summary>
    [Column("descripcion_Categoria")]
    [MaxLength(100)]
    public byte[] DescripcionCategoria { get; set; } = null!;

    /// <summary>
    /// Fecha y hora de creación de la categoría.
    /// </summary>
    [Column("fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Fecha y hora de la última actualización de la categoría (puede ser nulo).
    /// </summary>
    [Column("fecha_Actualizacion", TypeName = "datetime")]
    public DateTime? FechaActualizacion { get; set; }

    /// <summary>
    /// Colección de productos que pertenecen a esta categoría.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; }
}