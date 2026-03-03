using casefile.domain.model;

namespace casefile.application.DTOs.SchemaClient;

/// <summary>
/// DTO léger d'une définition d'attribut pour les dropdowns de sélection de schéma.
/// </summary>
public class DefinitionAttributForSelectDto
{
    public Guid Id { get; set; }
    public string Cle { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public TypeAttribut Type { get; set; }
    public bool EstRequis { get; set; }
    public string? ValeurDefaut { get; set; }
}
