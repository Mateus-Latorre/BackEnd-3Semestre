using ConnectPlus.Models;

namespace ConnectPlus.Interfaces;

public interface IContatoRepositorty
{
    List<Contato> Listar();
    void Cadastrar(Contato novoContato);
    void Atualizar(Guid id, Contato novoContato);
    void Deletar(Guid id, Contato novoContato);
    Contato BuscarPorId(Guid id);

}
