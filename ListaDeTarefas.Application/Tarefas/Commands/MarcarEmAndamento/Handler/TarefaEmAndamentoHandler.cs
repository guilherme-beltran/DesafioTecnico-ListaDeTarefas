﻿using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Response;
using ListaDeTarefas.Shared.Interfaces;
using System.Net;

namespace ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Handler
{
    public sealed class TarefaEmAndamentoHandler : ITarefaEmAndamentoHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarefasRepositorio _tarefaRepositorio;

        public TarefaEmAndamentoHandler(IUnitOfWork unitOfWork, ITarefasRepositorio tarefaRepositorio)
        {
            _unitOfWork = unitOfWork;
            _tarefaRepositorio = tarefaRepositorio;
        }

        public async Task<IResponse> Handle(TarefaEmAndamentoRequest request)
        {
            request.Validar();
            if (!request.IsValid)
            {
                return new TarefaEmAndamentoResponse(StatusCode: HttpStatusCode.BadRequest,
                                            Mensagem: "Falha na requisição para criar um usuário.",
                                            Notifications: request.Notifications);
            }

            try
            {
                var tarefa = await _tarefaRepositorio.ObterPorIdAsync(request.IdTarefa);
                if (tarefa is null)
                {
                    return new TarefaEmAndamentoResponse(StatusCode: HttpStatusCode.BadRequest,
                                                Mensagem: $"Tarefa informada não foi encontrada.",
                                                Notifications: request.Notifications);
                }

                tarefa.MarcarComoEmAndamento();

                _unitOfWork.BeginTransaction();
                await _tarefaRepositorio.AlterarStatus(tarefa);
                _unitOfWork.Commit();

                return new TarefaEmAndamentoResponse(StatusCode: HttpStatusCode.OK,
                                            Mensagem: $"Status da tarefa {request.IdTarefa} alterado para Em Andamento.",
                                            Notifications: request.Notifications);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
