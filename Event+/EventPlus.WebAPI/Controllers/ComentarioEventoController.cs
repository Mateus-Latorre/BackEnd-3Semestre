using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;
    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository) { 
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }
    /// <summary>
    /// Endpoint da API que cadastra e modera um comentário
    /// </summary>
    /// <param name="comentarioEvento">cometário a ser moderado</param>
    /// <returns>Status Code 201 e o comentário criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio");
            }

            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);
            var novoComentario = new ComentarioEvento
            {
                
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentarioEvento = DateTime.Now,
                Exibe = !temConteudoImproprio
            };
            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os comentários de um usuario em um evento
    /// </summary>
    /// <param name="idUsuario">id do usuário para a filtragem</param> 
    /// <param name="idEvento">id do evento para a filtragem</param> 
    /// <returns>Status code 200 e uma lista de comentários filtrada pelo usuário e evento</returns> 
    [HttpGet("BuscarPorIdUsuario/{idUsuario, idEvento}")]
    public IActionResult BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os comentários de um evento
    /// </summary>
    /// <param name="idEvento">id do evento para a filtragem</param> 
    /// <returns>Status Code 200 e uma lista de comentários filtrada pelo evento</returns> 
    [HttpGet("BuscarPorEvento/{idEvento}")]
    public IActionResult BuscarPorEvento(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de deletar um comentário pelo seu id
    /// </summary>
    /// <param name="id">id do comentário a ser deletado</param> 
    /// <returns>Status Code 201</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("ListarSomenteExibe/{idEvento}")]
    public IActionResult ListarSomenteExibe(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
