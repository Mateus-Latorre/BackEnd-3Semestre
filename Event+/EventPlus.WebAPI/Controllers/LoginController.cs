using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    [HttpPost]
    public IActionResult Login(LoginDTO loginDto)
    {
        try
        {
            if (loginDto.IdTipoUsuario == null)
            {
                return BadRequest("IdTipoUsuario é obrigatório.");
            }

            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(
                loginDto.Email!, 
                loginDto.Senha!, 
                loginDto.IdTipoUsuario.Value // Corrigido: uso de .Value para Guid? -> Guid
            );
            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha inválidos.");
            }
            //Caso encontre o usuário, prossegue para a criação do token
            //1. Definir as informações(Claims) que serão fornecidas no token (payload)
            var claims = new[]
            {
                    //forma da cliam
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                    //Existe a possibilidade de criar claims personalizada
                    //EX: new Claim("ClaimPersonalizada", "Valor da claim personalizada")
                };

            //2. Definir a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("events-chave-autenticacao-webapi-dev"));

            //3. Definir as credenciais do token(Header)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4. Gerar o token
            var token = new JwtSecurityToken(
                //emissor do token
                issuer: "api_events",
                //destinatário do token
                audience: "api_events",
                //dados definidos nas claims(informações)
                claims: claims,
                //tempo de expiração do token
                expires: DateTime.Now.AddMinutes(5),
                //credenciais do token
                signingCredentials: creds
            );
            //5. Retornar o token criado
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}

