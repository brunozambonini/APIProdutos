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
    public class CategoriesService : ICategoriesService
    {
        private readonly ProvaContext _context;

        public CategoriesService(ProvaContext context)
        {
            _context = context;
        }

        public void Add<Categorias>(Categorias entity)
        {
            _context.Add(entity);
        }

        public async Task<Categorias[]> GetAll()
        {
            IQueryable<Categorias> query = _context.Categorias
                .Include(x => x.Produtos);

            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
