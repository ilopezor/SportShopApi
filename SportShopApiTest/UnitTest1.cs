using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Application.DTO;
using Moq;

namespace SportShopTest
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductByIdAsync_ExistingId_ReturnsProduct()
        {
            // Arrange
            var mockProductRepository = new Mock<IRepository<Producto>>();
            var mockCategoryRepository = new Mock<IRepository<Categoria>>();
            var mockProductCategoryRepository = new Mock<IProductRepository>();
            var expectedProduct = new Producto { IdProducto = 1, Name = "Test Product" };
            mockProductRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedProduct);

            var productoService = new ProductService(mockProductRepository.Object, mockProductCategoryRepository.Object, mockCategoryRepository.Object);

            // Act
            var actualProduct = await productoService.GetProductByIdAsync(1);

            // Assert
            Assert.Equal(expectedProduct, actualProduct);
        }

        [Fact]
        public async Task CreateProductAsync_NonExistingCategory_ThrowsException()
        {
            // Arrange
            var mockProductRepository = new Mock<IRepository<Producto>>();
            var mockCategoryRepository = new Mock<IRepository<Categoria>>();
            var mockProductCategoryRepository = new Mock<IProductRepository>();
            var productModel = new ProductoDTO { Name = "New Product", Category = "NonExistent", Price = 10.0m, Stock = 5, Brand = "Brand" };

            mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Categoria>()); // No categorías existentes

            var productoService = new ProductService(mockProductRepository.Object, mockProductCategoryRepository.Object, mockCategoryRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => productoService.CreateProductAsync(productModel));
        }

        [Theory]
        [InlineData("Ropa")]
        [InlineData("Calzado")]
        public async Task GetProductsByCategoryNameAsync_ExistingCategory_ReturnsProducts(string categoryName)
        {
            // Arrange
            var mockProductRepository = new Mock<IRepository<Producto>>();
            var mockCategoryRepository = new Mock<IRepository<Categoria>>();
            var mockProductCategoryRepository = new Mock<IProductRepository>();

            mockProductCategoryRepository
                .Setup(repo => repo.GetProductsWithInclude())
                .ReturnsAsync(new List<Producto>
                { new Producto
                    {
                        IdProducto = 1,
                        Name = $"Product in {categoryName}",
                        IdCategoria = 1,
                        IdCategoriaNavigation = new Categoria { IdCategoria = 1, CodigoCategoria = categoryName }
                    }
                });

            var productoService = new ProductService(
                mockProductRepository.Object,
                mockProductCategoryRepository.Object,
                mockCategoryRepository.Object);

            // Act
            var actualProducts = await productoService.GetProductsByCategoryNameAsync(categoryName);

            // Assert
            Assert.NotEmpty(actualProducts);
            Assert.Equal(1, actualProducts.Count());
            Assert.Contains(actualProducts, p => p.Name.Contains(categoryName));
        }

        [Theory]
        [InlineData(1, "Updated Product")]
        [InlineData(2, "Another Updated Product")]
        public async Task UpdateProductAsync_ExistingProduct_CallsUpdateAsyncWithCorrectData(long productId, string newName)
        {
            // Arrange
            var mockProductRepository = new Mock<IRepository<Producto>>();
            var mockCategoryRepository = new Mock<IRepository<Categoria>>();
            var mockProductCategoryRepository = new Mock<IProductRepository>();
            var existingProduct = new Producto { IdProducto = productId, Name = "Original Name", IdCategoria = 1 };
            var mockCategoryEntity = new Categoria { IdCategoria = 1, CodigoCategoria = "Test" };
            var productModel = new ProductoDTO { Name = newName, Category = "Test", Price = 10.0m, Stock = 5, Brand = "Brand" };

            mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Categoria> { mockCategoryEntity });
            mockProductRepository.Setup(repo => repo.UpdateAsync(It.Is<Producto>(p => p.IdProducto == productId && p.Name == newName))).Returns(Task.CompletedTask);

            var productoService = new ProductService(mockProductRepository.Object, mockProductCategoryRepository.Object, mockCategoryRepository.Object);

            // Act
            await productoService.UpdateProductAsync(productId, productModel);

            // Assert
            mockProductRepository.Verify(repo => repo.UpdateAsync(It.Is<Producto>(p => p.IdProducto == productId && p.Name == newName)), Times.Once);
        }
    }
}