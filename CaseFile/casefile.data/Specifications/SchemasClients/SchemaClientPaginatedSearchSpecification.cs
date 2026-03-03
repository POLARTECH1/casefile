using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.SchemasClients;

/// <summary>
/// Spécification paginée pour rechercher des schémas clients par nom, avec chargement
/// des définitions associées. Utilisée pour les dropdowns de sélection de schéma.
/// </summary>
public sealed class SchemaClientPaginatedSearchSpecification : Specification<SchemaClient>
{
    public SchemaClientPaginatedSearchSpecification(string? searchTerm, int pageNumber, int pageSize)
    {
        Query.Include(s => s.Definitions);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var normalized = searchTerm.Trim().ToLower();
            Query.Where(s => s.Nom.ToLower().Contains(normalized));
        }

        var normalizedPage = pageNumber < 1 ? 1 : pageNumber;
        var normalizedSize = pageSize < 1 ? 10 : pageSize;
        Query.Skip((normalizedPage - 1) * normalizedSize).Take(normalizedSize);
    }
}
