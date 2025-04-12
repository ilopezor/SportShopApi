using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AppDbContext
{
    /// <summary>
    /// Contexto de Entity Framework Core para la base de datos de la aplicación.
    /// </summary>
    public partial class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor con opciones para ApplicationDbContext.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            if (!this.Database.CanConnect())
            {
                throw new Exception("No se puede conectar a la base de datos.");
            }
        }

        /// <summary>
        /// DbSet que representa la tabla de Categorías en la base de datos.
        /// </summary>
        public virtual DbSet<Categoria> Categoria { get; set; }

        /// <summary>
        /// DbSet que representa la tabla de Productos en la base de datos.
        /// </summary>
        public virtual DbSet<Producto> Productos { get; set; }

        /// <summary>
        /// Define el esquema de la base de datos y sus relaciones.
        /// </summary>
        /// <param name="modelBuilder">Constructor para definir el modelo de la base de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");

                entity.Property(e => e.CodigoCategoria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo_Categoria");

                entity.Property(e => e.DescripcionCategoria)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion_Categoria");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Actualizacion");

                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto).HasName("PK__Producto__DB05CEDB78BD7A51");

                entity.Property(e => e.IdProducto).HasColumnName("id_Producto");

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("brand");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Actualizacion");

                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_Creacion");

                entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productos_Categoria");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        /// <summary>
        /// Método parcial para extender la configuración del modelo de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">Constructor para definir el modelo de la base de datos.</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
