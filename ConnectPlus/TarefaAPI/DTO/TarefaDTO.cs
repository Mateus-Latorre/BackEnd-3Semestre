namespace TarefaAPI.DTO;

public class TarefaDTO
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public bool Conclusao { get; set; }
    public DateTime DataCriacao { get; set; }
}
