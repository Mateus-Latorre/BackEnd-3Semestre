using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
        void Cadastrar(UsuarioDTO usuario);
        UsuarioDTO BuscarPorId(Guid id);
        UsuarioDTO BuscarPorEmailESenha(string email, string senha);
}
