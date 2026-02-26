using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.SchemasClients;

public sealed class SchemaClientWithDefinitionsSpecification : Specification<SchemaClient>
{
    public SchemaClientWithDefinitionsSpecification()
    {
        Query.Include(s => s.Definitions);
    }
}
