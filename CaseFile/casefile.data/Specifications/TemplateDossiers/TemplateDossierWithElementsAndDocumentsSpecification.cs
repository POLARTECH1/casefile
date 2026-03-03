using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.TemplateDossiers;

/// <summary>
/// Classe de spécification utilisée pour inclure les éléments et les documents attendus d'un TemplateDossier.
/// Permet d'enrichir un TemplateDossier en incluant ses éléments associés ainsi que les documents attendus liés à ces éléments.
/// </summary>
public sealed class TemplateDossierWithElementsAndDocumentsSpecification : Specification<TemplateDossier>
{
    /// <summary>
    /// Spécification pour inclure les éléments et les documents attendus associés à un TemplateDossier.
    /// Utilisé pour récupérer un TemplateDossier enrichi de ses éléments et leurs documents attendus.
    /// </summary>
    public TemplateDossierWithElementsAndDocumentsSpecification()
    {
        Query.Include(t => t.Elements)
            .ThenInclude(e => e.DocumentsAttendus);
    }
}
