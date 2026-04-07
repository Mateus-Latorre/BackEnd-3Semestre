using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarefaAPI.DTO;
using TarefaAPI.Interface;
using TarefaAPI.Models;

namespace TarefaAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ITarefaRepository _tarefaRepository;
    public TarefaController(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tarefaRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    [HttpGet("{Id}")]
    public IActionResult BuscarPorId(Guid Id)
    {
        try
        {
            return Ok(_tarefaRepository.BuscarPorId(Id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
     [HttpPost]
     public IActionResult Cadastrar([FromForm] TarefaDTO tarefa)
        {
            try
            {
            var novaTarefa = new Tarefa
            {

                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Conclusao = tarefa.Conclusao,
                DataCriacao = tarefa.DataCriacao
            };
                _tarefaRepository.Cadastrar(novaTarefa);
                return Ok(novaTarefa);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
    }
    [HttpPut("{Id}")]
    public IActionResult Atualizar(Guid Id, [FromForm] TarefaDTO tarefa)
    {
        try
        {
            var tarefaAtualizada = new Tarefa
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Conclusao = tarefa.Conclusao,
                DataCriacao = tarefa.DataCriacao
            };
            _tarefaRepository.Atualizar(Id, tarefaAtualizada);
            return Ok(tarefaAtualizada);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    [HttpDelete("{Id}")]
    public IActionResult Deletar(Guid Id)
    {
        try
        {
            _tarefaRepository.Deletar(Id);
            return Ok("Tarefa deletada com sucesso.");
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    
}
