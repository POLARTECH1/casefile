using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.Clients;

public sealed class ClientByIdWithDetailsSpecification : Specification<Client>
{
    public ClientByIdWithDetailsSpecification(Guid clientId)
    {
        Query.Where(c => c.Id == clientId)
            .Include(c => c.SchemaClient)
            .Include(c => c.Dossiers)
            .ThenInclude(d => d.Documents);
    }
}
