using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductsService
    {
        void Add<Produtos>(Produtos entity);
        void Update<Produtos>(Produtos entity);
        void Delete<Produtos>(Produtos entity);

        Task<bool> SaveChangeAsync();

        Task<Produtos[]> GetAll(string term, int tamanhoPagina, int paginaAtual);

        Task<Produtos> GetProdutoById(long id);
    }
}
