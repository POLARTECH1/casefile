using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.TemplateDossiers;

/// <summary>
/// Représente une spécification permettant de récupérer un template de dossier, ses éléments associés,
/// ainsi que les documents attendus pour chaque élément, en fonction de l'identifiant unique du template.
/// </summary>
/// <remarks>
/// Cette classe est utilisée pour définir une requête incluant les éléments et documents attendus associés
/// à un <c>TemplateDossier</c>. Elle repose sur le cadre d'utilisation de la bibliothèque Ardalis.Specification.
/// </remarks>
public sealed class TemplateDossierByIdWithElementsAndDocumentsSpecification : Specification<TemplateDossier>
{
    
    public TemplateDossierByIdWithElementsAndDocumentsSpecification(Guid templateDossierId)
    {
        Query.Where(t => t.Id == templateDossierId)
            .Include(t => t.Elements)
            .ThenInclude(e => e.DocumentsAttendus);
    }
}
