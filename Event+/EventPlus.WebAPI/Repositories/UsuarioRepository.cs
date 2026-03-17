using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Busca um usuário pelo email e valida o hash da senha.
    /// </summary>
    /// <param name="Email">Email do usuário a ser buscado</param> 
    /// <param name="Senha">Senha para validar o usuário</param> 
    /// <returns>Usuário buscado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha, Guid IdTipoUsuario)
    {
        //Primeiro, buscamos o usuário pelo email
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);
        //Verificamos se o usuário foi encontrado
        if (usuarioBuscado != null)
        {
            //Comparamos o hash da senha digitada com o que está no banco
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);
            if (confere)
            {
                return usuarioBuscado;
            }
        }
        return null!;
    }
    /// <summary>
    /// Busca um usuário peo id, incluindo os dados do seu Tipo de Usuário
    /// </summary>
    /// <param name="id">id od usuário a ser buscado</param>
    /// <returns>Usuário Buscado e seu tipo de usuário</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }
    /// <summary>
    /// Cadastra um novo usuário. A senha é criptografada e o Id gerado pelo banco.
    /// </summary>
    /// <param name="usuario">Usuário a ser cadastro</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);
       _context.Usuarios.Add(usuario);
       _context.SaveChanges();
    }
}
