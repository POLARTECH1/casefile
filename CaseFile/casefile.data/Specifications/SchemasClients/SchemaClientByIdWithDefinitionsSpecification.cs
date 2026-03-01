using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.SchemasClients;

/// <summary>
/// Spécification utilisée pour récupérer un objet <c>SchemaClient</c>
/// correspondant à un identifiant spécifique et incluant les définitions associées.
/// </summary>
/// <remarks>
/// Cette spécification permet d'effectuer une requête filtrée sur l'entité
/// <c>SchemaClient</c>, en incluant les relations de navigation vers
/// les définitions associées.
/// </remarks>
/// <example>
/// Cette classe est généralement utilisée dans des cas où il est nécessaire
/// de charger un <c>SchemaClient</c> assorti de ses définitions, par exemple
/// dans des cas d'affichage détaillé ou d'analyse.
/// </example>
public sealed class SchemaClientByIdWithDefinitionsSpecification : Specification<SchemaClient>
{
    public SchemaClientByIdWithDefinitionsSpecification(Guid schemaClientId)
    {
        Query.Where(s => s.Id == schemaClientId)
            .Include(s => s.Definitions);
    }
}
