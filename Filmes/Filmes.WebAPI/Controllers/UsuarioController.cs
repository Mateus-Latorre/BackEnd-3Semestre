using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
	private readonly IUsuarioRepository _usuarioRepository;

	public UsuarioController(IUsuarioRepository usuarioRepository)
	{
		_usuarioRepository = usuarioRepository;
	}

	[HttpGet("{id}")]
	public IActionResult GetById(Guid id)
	{
		try
		{
			return Ok(_usuarioRepository.BuscarPorId(id));
		}
		catch (Exception erro)
		{
			return BadRequest(erro.Message);
		}
    }

    [HttpPost]
	public IActionResult Post(Usuario novoUsuario)
	{

		try
		{
			_usuarioRepository.Cadastrar(novoUsuario);
			return StatusCode(201, novoUsuario);
		}
		catch (Exception erro)
		{
			return BadRequest(erro.Message);
		}

	}
}

