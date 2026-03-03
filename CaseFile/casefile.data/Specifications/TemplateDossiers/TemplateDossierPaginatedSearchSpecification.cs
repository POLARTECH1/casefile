using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.TemplateDossiers;

/// <summary>
/// Spécification paginée pour rechercher des templates de dossier par nom.
/// Utilisée pour les dropdowns de sélection de template.
/// </summary>
public sealed class TemplateDossierPaginatedSearchSpecification : Specification<TemplateDossier>
{
    public TemplateDossierPaginatedSearchSpecification(string? searchTerm, int pageNumber, int pageSize)
    {
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var normalized = searchTerm.Trim().ToLower();
            Query.Where(t => t.Nom.ToLower().Contains(normalized));
        }

        var normalizedPage = pageNumber < 1 ? 1 : pageNumber;
        var normalizedSize = pageSize < 1 ? 10 : pageSize;
        Query.Skip((normalizedPage - 1) * normalizedSize).Take(normalizedSize);
    }
}
