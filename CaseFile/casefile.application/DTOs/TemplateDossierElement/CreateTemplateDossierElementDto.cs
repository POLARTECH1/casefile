using casefile.application.DTOs.DocumentAttendu;

namespace casefile.application.DTOs.TemplateDossierElement;

/// <summary>
/// DTO utilise pour creer un element de template dossier.
/// </summary>
public class CreateTemplateDossierElementDto
{
    /// <summary>
    /// Nom de l'élément du template dossier.
    /// Ce champ est obligatoire et sa longueur maximale est de 150 caractères.
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Identifiant du parent de l'élément du template dossier.
    /// Ce champ est optionnel et utilisé pour représenter la hiérarchie des éléments.
    /// </summary>
    public Guid? IdParent { get; set; }

    /// <summary>
    /// Ordre d'affichage de l'élément dans le template dossier.
    /// Sa valeur doit être supérieure ou égale à 0.
    /// </summary>
    public int Ordre { get; set; }

    /// <summary>
    /// Liste des documents attendus associés à cet élément du template dossier.
    /// </summary>
    public List<CreateDocumentAttenduDto> CreateDocumentAttendusDto { get; set; } = new();
}