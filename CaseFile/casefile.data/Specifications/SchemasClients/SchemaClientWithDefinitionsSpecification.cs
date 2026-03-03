using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.SchemasClients;

/// <summary>
/// Représente une spécification pour récupérer les entités <c>SchemaClient</c>
/// avec leurs définitions associées. Cette spécification inclut
/// automatiquement la navigation vers la collection <c>Definitions</c>.
/// </summary>
public sealed class SchemaClientWithDefinitionsSpecification : Specification<SchemaClient>
{
    public SchemaClientWithDefinitionsSpecification()
    {
        Query.Include(s => s.Definitions);
    }
}
