using System.Text.Json.Serialization;

public record class AvailableItens
{
    public List<Resultado>? resultado {get; set;}
}

public class Resultado 
{
   public string? codsite_fidelidade_itens {get; set;}
   public string? sfi_descricao {get; set;}
   public string? esgotado {get; set;}
}
   