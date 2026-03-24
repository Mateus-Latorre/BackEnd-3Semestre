namespace ConnectPlus.DTO;

public class ContatoDTO
{
    public string? Nome { get; set; }
    public string? FormaDeContato { get; set; }
    public IFormFile? Imagem { get; set; }
    public Guid IdTipoContato { get; set; }
}
