﻿using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Handler;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Handler;
using ListaDeTarefas.Infra.Data.Context;
using ListaDeTarefas.Infra.Queries;
using ListaDeTarefas.Infra.Repositories;
using ListaDeTarefas.Infra.Services;
using Microsoft.EntityFrameworkCore;
using ListaDeTarefas.Domain;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Handler;

namespace ListaDeTarefas.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            #region Connection

            builder.Services.AddDbContext<TarefasDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Database"]));

            #endregion 

            #region Services

            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ITokenServices, TokenServices>();

            #endregion

            AddRepositories(builder);
        }

        private static void AddRepositories(WebApplicationBuilder builder)
        {
            #region Unit of Work

            builder.Services.AddScoped(serviceType: typeof(IRepositorioBase<>), implementationType: typeof(RepositorioBase<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Usuario

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddScoped<ICriarUsuarioHandler, CriarUsuarioHandler>();
            builder.Services.AddScoped<IExcluirUsuarioHandler, ExcluirUsuarioHandler>();
            builder.Services.AddScoped<IAlterarSenhaHandler, AlterarSenhaHandler>();
            builder.Services.AddScoped<ILogarHandler, LogarHandler>();

            #endregion

            #region Tarefa

            builder.Services.AddScoped<ITarefasRepositorio, TarefasRepositorio>();

            builder.Services.AddScoped<ICriarTarefaHandler, CriarTarefaHandler>();
            builder.Services.AddScoped<ITarefaEmAndamentoHandler, TarefaEmAndamentoHandler>();
            builder.Services.AddScoped<IFinalizarTarefaHandler, FinalizarTarefaHandler>();

            builder.Services.AddScoped<ITarefasQueries, TarefasQueries>();

            #endregion

        }
    }
}
