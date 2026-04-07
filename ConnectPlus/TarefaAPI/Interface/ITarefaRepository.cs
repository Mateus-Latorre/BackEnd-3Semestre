using TarefaAPI.Models;

namespace TarefaAPI.Interface;

public interface ITarefaRepository
{
    void Cadastrar(Tarefa tarefa);
    void Atualizar(Guid id, Tarefa tarefa);
    void Deletar(Guid id);
    Tarefa BuscarPorId(Guid id);
    List<Tarefa> Listar();
}
