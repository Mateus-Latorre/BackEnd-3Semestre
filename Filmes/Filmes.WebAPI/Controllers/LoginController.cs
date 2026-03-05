using Microsoft.AspNetCore.Http;
using Filmes.WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Filmes.WebAPI.Models;
using Filmes.WebAPI.DTO;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Filmes.WebAPI.Controllers
{
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
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmaileSenha(loginDto.Email!, loginDto.Senha!);
                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos.");
                }
                //Caso encontre o usuário, prossegue para a criação do token
                //1. Definir as informações(Claims) que serão fornecidas no token (payload)
                var claims = new[]
                {
                    //forma da cliam
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                    //Existe a possibilidade de criar claims personalizada
                    //EX: new Claim("ClaimPersonalizada", "Valor da claim personalizada")
                };

                //2. Definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3. Definir as credenciais do token(Header)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4. Gerar o token
                var token = new JwtSecurityToken(
                    //emissor do token
                    issuer: "api_filmes",
                    //destinatário do token
                    audience: "api_filmes",
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
}
