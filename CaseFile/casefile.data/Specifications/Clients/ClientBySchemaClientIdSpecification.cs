using Ardalis.Specification;
using casefile.domain.model;
using Microsoft.EntityFrameworkCore;

namespace casefile.data.Specifications.Clients;

public sealed class ClientBySchemaClientIdSpecification : Specification<Client>
{
    public ClientBySchemaClientIdSpecification(Guid schemaClientId)
    {
        Query.Where(c => EF.Property<Guid?>(c, "SchemaClientId") == schemaClientId);
    }
}
