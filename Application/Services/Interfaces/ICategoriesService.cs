using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICategoriesService
    {
        void Add<Categorias>(Categorias entity);

        Task<bool> SaveChangeAsync();

        Task<Categorias[]> GetAll();
    }
}
