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
    public class UsuariosService : IUsuariosService
    {
        private readonly ProvaContext _context;

        public UsuariosService(ProvaContext context)
        {
            _context = context;
        }

        public async Task<Usuarios> DoLogin(string email, string senha)
        {
            IQueryable<Usuarios> query = _context.Usuarios;

            query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Email == email && x.Senha == senha);
        }
    }
}
