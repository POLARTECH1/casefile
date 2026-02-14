using Ardalis.Specification;

namespace casefile.data.Specifications;

/// <summary>
/// Représente une spécification paginée qui permet de gérer la pagination
/// des résultats dans une requête.
/// </summary>
/// <typeparam name="TData">
/// Le type de données pour lequel cette spécification est appliquée. Ce type
/// doit être une classe.
/// </typeparam>
public class PaginatedSpecification<TData> : Specification<TData> where TData : class
{
    public PaginatedSpecification(int pageNumber, int pageSize)
    {
        var normalizedPageNumber = pageNumber < 1 ? 1 : pageNumber;
        var normalizedPageSize = pageSize < 1 ? 10 : pageSize;

        Query.Skip((normalizedPageNumber - 1) * normalizedPageSize)
            .Take(normalizedPageSize);
    }
}
