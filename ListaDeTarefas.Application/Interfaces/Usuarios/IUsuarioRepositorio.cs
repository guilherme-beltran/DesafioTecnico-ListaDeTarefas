﻿using ListaDeTarefas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface IUsuarioRepositorio
    {
        Task AdicionarAsync(Usuario usuario);
        Task<bool> RemoverAsync(int id);
        Task<Usuario> BuscarPorIdAsync(int id);
    }
}
