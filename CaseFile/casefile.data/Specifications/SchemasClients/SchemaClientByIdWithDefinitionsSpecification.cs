using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.SchemasClients;

public sealed class SchemaClientByIdWithDefinitionsSpecification : Specification<SchemaClient>
{
    public SchemaClientByIdWithDefinitionsSpecification(Guid schemaClientId)
    {
        Query.Where(s => s.Id == schemaClientId)
            .Include(s => s.Definitions);
    }
}
