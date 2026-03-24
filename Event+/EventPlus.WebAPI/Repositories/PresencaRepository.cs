using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;
    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }
    /// <summary>
    /// Atualiza uma presença existente, caso o id do evento ou do usuário seja vazio, mantém os valores anteriores
    /// </summary>
    /// <param name="id">id da presença a ser atualizada</param> 
    /// <param name="presenca">presença com os dados atualizados</param> 
    public void Atualizar(Guid id, Presenca presenca)
    {
       var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;
            _eventContext.SaveChanges();
        }
    }
    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param> 
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == id)!;
    }
    /// <summary>
    /// Deleta uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser deletada</param> 
    public void Deletar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
            _eventContext.SaveChanges();
        }
    }
    /// <summary>
    /// Adiciona uma nova presença
    /// </summary>
    /// <param name="Inscricao">Dados da presença a ser adicionada</param> 
    public void Inscrever(Presenca Inscricao)
    {
        _eventContext.Presencas.Add(Inscricao);
        _eventContext.SaveChanges();
    }
    /// <summary>
    /// Lista todas as presenças
    /// </summary>
    /// <returns>Uma lista de todas as presenças</returns>
    public List<Presenca> Listar()
    {
        return _eventContext.Presencas.ToList();
    }
    /// <summary>
    /// Lista de presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">id do usuário para a filtragem</param>
    /// <returns>uma lista de presencas de um usuário específico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
    }
}
