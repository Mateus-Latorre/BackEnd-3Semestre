using ConnectPlus.BdContextEvent;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class ContatoRepository : IContatoRepositorty
{
    private readonly ConnectContext _context;
    public ContatoRepository(ConnectContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Contato novoContato)
    {
        
    }

    public Contato BuscarPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Cadastrar(Contato novoContato)
    {
        throw new NotImplementedException();
    }

    public void Deletar(Guid id, Contato novoContato)
    {
        throw new NotImplementedException();
    }

    public List<Contato> Listar()
    {
        throw new NotImplementedException();
    }
}
