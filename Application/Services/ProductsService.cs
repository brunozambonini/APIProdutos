using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProvaContext _context;

        public ProductsService(ProvaContext context)
        {
            _context = context;
        }
        public void Add<Produtos>(Produtos entity)
        {
            _context.Add(entity);
        }

        public void Delete<Produtos>(Produtos entity)
        {
            _context.Remove(entity);
        }
        public void Update<Produtos>(Produtos entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Produtos[]> GetAll(string term, int page, int pageSize)
        {
            IQueryable<Produtos> query = _context.Produtos
                .Include(x => x.Categoria);

            query = query.AsNoTracking()
                .Where(x => x.Nome.Contains(term) || x.Categoria.Nome.Contains(term));

            int qtdProdutos = query.Count();
            int qunatidadePaginas = Convert.ToInt32(Math.Ceiling(qtdProdutos * 1M / pageSize));

            if(page > qunatidadePaginas)
            {
                return null;
            }

            // Pula uma qunatidade de registros de acordo com a pagina
            // Se for a primeira página, pega os primeiros registros, se for a segunda, pula as primeiras que foi pego na primeira página...
            query = query.Skip(pageSize * (page - 1)).Take(pageSize);
            return await query.ToArrayAsync();
        }

        public async Task<Produtos> GetProdutoById(long id)
        {
            IQueryable<Produtos> query = _context.Produtos;

            query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
