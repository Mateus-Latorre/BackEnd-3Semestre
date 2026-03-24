using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresencaRepository _presencaRepository;
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }
    /// <summary>
    /// Endpoint da API que retorna uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param> 
    /// <returns>Status code 200 e presença busacada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que retorna uma lista de presenças filtrada por usuário
    /// </summary>
    /// <param name="idUsuario"></param> id do usuário para filtragem
    /// <returns>Status code 200 e uma lista de presenças filtrada pelo usuário</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult ListarMinhas(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que chama o método de inscrição de presença
    /// </summary>
    /// <param name="presenca">dados da presença a ser inscrita</param> 
    /// <returns>Status code 201 e a presença que foi inscrita</returns> 
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        var novaPresenca = new Presenca
        {
            IdUsuario = presenca.IdUsuario,
            IdEvento = presenca.IdEvento,
            Situacao = presenca.Situacao
        };
        _presencaRepository.Inscrever(novaPresenca);
        return StatusCode(201, novaPresenca);
    }
    /// <summary>
    /// Endpoint da API que retorna uma lista de todas as presenças cadastradas
    /// </summary>
    /// <returns>Status code 200 e uma lista de todas as presenças cadastradas</returns> 
    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_presencaRepository.Listar());
    }
    /// <summary>
    /// Endpoint da API que chama o método de atualização de presença
    /// </summary>
    /// <param name="id">id da presença a ser atualizadaparam> 
    /// <param name="presenca">dados da presença a ser atualizada</param>   
    /// <returns>Status code 204 e presença atualizada</returns> 
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presenca)
    {
        try
        {
            var presencaAtualizada = new Presenca
            {
                IdUsuario = presenca.IdUsuario,
                IdEvento = presenca.IdEvento,
                Situacao = presenca.Situacao
            };
            _presencaRepository.Atualizar(id, presencaAtualizada);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que chama o método de deletar uma presença
    /// </summary>
    /// <param name="id">id da presença a ser deletada</param> 
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
