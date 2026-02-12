using casefile.domain.model;
using casefile.data.Specifications;

namespace casefile.data.Specifications.Clients;

public sealed class ClientByNomPaginatedSpecification : ByNamePaginatedSpecification<Client>
{
    public ClientByNomPaginatedSpecification(string? nom, int pageNumber, int pageSize)
        : base(c => c.Nom, nom, pageNumber, pageSize)
    {
    }
}
