using casefile.domain.model;

namespace casefile.application.DTOs.DefinitionAttribut;

/// <summary>
/// DTO utilise pour creer une definition d'attribut.
/// </summary>
public class CreateDefinitionAttributDto
{
    public string Cle { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public TypeAttribut Type { get; set; }
    public bool EstRequis { get; set; }
    public string? ValeurDefaut { get; set; }
}
