﻿using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUsuariosService
    {

        Task<Usuarios> DoLogin(string email, string senha);

    }
}
