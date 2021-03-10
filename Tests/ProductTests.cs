using Application.Context;
using Application.Entities;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ProductTests
    {
        // Para funcionar os testes precisa comentar o 
        // public ProvaContext(DbContextOptions<ProvaContext> options) : base(options)
        // se não da erro de construtor.
        [TestMethod]
        public void ShouldCreateNewProduct()
        {
            // Arrange
            var _dbSet = new Mock<DbSet<Produtos>>();

            var _context = new Mock<ProvaContext>();
            _context.Setup(x => x.Produtos).Returns(_dbSet.Object);

            var _service = new ProductsService(_context.Object);

            var sampleProduct = new Produtos()
            {
                Nome = "Teste",
                CategoriaId = 1,
                PrecoUnitario = 50,
                QuantidadeEstoque = 10,
                Status = true
            };

            _service.Add(sampleProduct);

            _dbSet.Verify(m => m.Add(It.IsAny<Produtos>()), Times.Once());
            _context.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void ShouldGetAllProducts()
        {
            var sampleData = new List<Produtos>()
            {
                new Produtos() { Id = 1, Nome = "Computador", CategoriaId = 1, PrecoUnitario = 3500, QuantidadeEstoque = 4, Status = true},
                new Produtos() { Id = 2, Nome = "Jogo de Talher", CategoriaId = 2, PrecoUnitario = 60, QuantidadeEstoque = 31, Status = false}
            }.AsQueryable();

            var _dbSet = new Mock<DbSet<Produtos>>();
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.Provider).Returns(sampleData.Provider);
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.Expression).Returns(sampleData.Expression);
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.ElementType).Returns(sampleData.ElementType);
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.GetEnumerator()).Returns(sampleData.GetEnumerator());

            var _context = new Mock<ProvaContext>();
            _context.Setup(x => x.Produtos).Returns(_dbSet.Object);

            var _service = new ProductsService(_context.Object);

            var produtos = _service.GetAll("com",1,1);

            Assert.IsNotNull(produtos);
        }


        [TestMethod]
        public void ShouldGetProductById()
        {
            var sampleData = new List<Produtos>()
            {
                new Produtos() { Id = 1, Nome = "Computador", CategoriaId = 1, PrecoUnitario = 3500, QuantidadeEstoque = 4, Status = true},
                new Produtos() { Id = 2, Nome = "Jogo de Talher", CategoriaId = 2, PrecoUnitario = 60, QuantidadeEstoque = 31, Status = false}
            }.AsQueryable();

            var _dbSet = new Mock<DbSet<Produtos>>();
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.Provider).Returns(sampleData.Provider);
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.Expression).Returns(sampleData.Expression);
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.ElementType).Returns(sampleData.ElementType);
            _dbSet.As<IQueryable<Produtos>>().Setup(x => x.GetEnumerator()).Returns(sampleData.GetEnumerator());

            var _context = new Mock<ProvaContext>();
            _context.Setup(x => x.Produtos).Returns(_dbSet.Object);

            var _service = new ProductsService(_context.Object);

            var produtos = _service.GetProdutoById(1);

            Assert.IsNotNull(produtos);
        }
    }
}
