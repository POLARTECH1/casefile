using Ardalis.Specification;

namespace casefile.data.Specifications;

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
