using TarefaAPI.BdContextTarefa;
using TarefaAPI.Interface;
using TarefaAPI.Models;

namespace TarefaAPI.Repository;

public class TarefaRepository : ITarefaRepository
{
    private readonly TarefaContext _context;

    public TarefaRepository(TarefaContext context)
    {
        _context = context;

    }

    public void Atualizar(Guid id, Tarefa tarefa)
    {
       var tarefaExistente = _context.Tarefas.Find(id);
        if (tarefaExistente == null)
        {
            throw new Exception("Tarefa não encontrada.");
        }
        tarefaExistente.Titulo = String.IsNullOrWhiteSpace(tarefa.Titulo) ? tarefaExistente.Titulo : tarefa.Titulo;
        tarefaExistente.Descricao = String.IsNullOrWhiteSpace(tarefa.Descricao) ? tarefaExistente.Descricao : tarefa.Descricao;
        tarefaExistente.DataCriacao = tarefa.DataCriacao;
        tarefaExistente.Conclusao = tarefa.Conclusao;
        _context.Tarefas.Update(tarefaExistente);
        _context.SaveChanges();
    }

    public Tarefa BuscarPorId(Guid id)
    {
        return _context.Tarefas.Find(id)!;
    }

    public void Cadastrar(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var tarefaExistente = _context.Tarefas.Find(id);
        if (tarefaExistente == null)
        {
            throw new Exception("Tarefa não encontrada.");
        }
        _context.Tarefas.Remove(tarefaExistente);
        _context.SaveChanges();
    }

    public List<Tarefa> Listar()
    {
       return _context.Tarefas.ToList();
    }
}
