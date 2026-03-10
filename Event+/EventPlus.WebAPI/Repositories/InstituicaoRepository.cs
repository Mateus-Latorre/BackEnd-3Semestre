using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Atualiza os dados de uma instituição já existente
    /// </summary>
    /// <param name="id">id da instituição a ser atualizada</param> 
    /// <param name="instituicao">objeto com os dados atualizados da instituição</param>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBusacada = _context.Instituicaos.Find(id);

        if (instituicaoBusacada != null)
        {
            instituicaoBusacada.NomeFantasia = instituicao.NomeFantasia;
            instituicaoBusacada.Endereco = instituicao.Endereco;
            instituicaoBusacada.Cnpj = instituicao.Cnpj;
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca uma instituição pelo seu id
    /// </summary>
    /// <param name="id">id da instituição a ser buscada</param> 
    /// <returns>Objeto da instituição encontrada com as informações da instituição buscada</returns> 
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }
    /// <summary>
    /// Cadastra uma nova instituição
    /// </summary>
    /// <param name="instituicao">informações da instituição a ser cadastrada</param> 
    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }
    /// <summary>
    /// Deleta uma instituição
    /// </summary>
    /// <param name="id">id da instituição a ser deletada</param> 
    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);
        if(instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca uma lista de instituições cadastradas
    /// </summary>
    /// <returns>Uma lista de instituições</returns> 
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.ToList();
    }
}
