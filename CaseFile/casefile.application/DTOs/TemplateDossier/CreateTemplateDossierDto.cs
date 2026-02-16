using casefile.application.DTOs.TemplateDossierElement;

namespace casefile.application.DTOs.TemplateDossier;

/// <summary>
/// DTO utilise pour creer un template dossier.
/// </summary>
public class CreateTemplateDossierDto
{
    /// <summary>
    /// Propriété représentant le nom du template de dossier.
    /// </summary>
    /// <remarks>
    /// Le nom est une chaîne de caractères obligatoire, utilisée pour identifier le template.
    /// </remarks>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Propriété représentant la description du template de dossier.
    /// </summary>
    /// <remarks>
    /// La description est une chaîne de caractères optionnelle, permettant de fournir des informations supplémentaires sur le template.
    /// La longueur maximale de la description est de 2000 caractères.
    /// </remarks>
    public string? Description { get; set; }

    /// <summary>
    /// Propriété représentant la liste des éléments du template de dossier.
    /// </summary>
    /// <remarks>
    /// Cette propriété contient une collection d'objets du type CreateTemplateDossierElementDto,
    /// utilisée pour définir les éléments structurés d'un template de dossier.
    /// </remarks>

    public List<CreateTemplateDossierElementDto> CreateTemplateDossierElementDtos { get; set; } = new();
}