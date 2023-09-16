﻿using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request;
using ListaDeTarefas.Application.Tarefas.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> ListarTodas(
            [FromServices] ITarefasQueries queries)
        {
            var tarefas = await queries.ListarTodas();
            if (tarefas is null)
            {
                return NoContent();
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/TarefasFinalizadas")]
        public async Task<IActionResult> ListarTodasFinalizadas(
            [FromServices] ITarefasQueries queries)
        {
            var tarefas = await queries.ListarTodasFinalizadas();
            if (tarefas is null)
            {
                return NoContent();
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/TarefasEmAndamento")]
        public async Task<IActionResult> ListarTodasEmAndamento(
            [FromServices] ITarefasQueries queries)
        {
            var tarefas = await queries.ListarTodasEmAndamento();
            if (tarefas is null)
            {
                return NoContent();
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> Listar(
            [FromServices] ITarefasQueries queries,
            [FromRoute] int id)
        {
            var tarefa = await queries.ListarTarefa(id);
            if (tarefa is null)
            {
                return BadRequest($"Tarefa não encontrada.");
            }
            return Ok(tarefa);
        }

        [HttpGet]
        [Route("/ListarFinalizada/{id}")]
        public async Task<IActionResult> ListarFinalizada(
            [FromServices] ITarefasQueries queries,
            [FromRoute] int id)
        {
            var tarefa = await queries.ListarTarefaFinalizada(id);
            if (tarefa is null)
            {
                return BadRequest($"Tarefa não encontrada.");
            }
            return Ok(tarefa);
        }

        [HttpGet]
        [Route("/ListarEmAndamento/{id}")]
        public async Task<IActionResult> ListarEmAndamento(
            [FromServices] ITarefasQueries queries,
            [FromRoute] int id)
        {
            var tarefa = await queries.ListarTarefaEmAndamento(id);
            if (tarefa is null)
            {
                return BadRequest($"Tarefa não encontrada.");
            }
            return Ok(tarefa);
        }


        [HttpPost]
        [Route("/CriarTarefa")]
        public async Task<IActionResult> Criar(
            [FromServices] ICriarTarefaHandler _handler,
            [FromBody] CriarTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/TarefaEmAndamento")]
        public async Task<IActionResult> AlterarStatus(
            [FromServices] ITarefaEmAndamentoHandler _handler,
            [FromBody] TarefaEmAndamentoRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/Finalizar")]
        public async Task<IActionResult> FinalizarTarefa(
            [FromServices] IFinalizarTarefaHandler _handler,
            [FromBody] FinalizarTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
