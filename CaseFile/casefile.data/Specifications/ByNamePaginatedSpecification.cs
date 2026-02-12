using Ardalis.Specification;
using System.Linq.Expressions;

namespace casefile.data.Specifications;

public class ByNamePaginatedSpecification<TData> : Specification<TData> where TData : class
{
    public ByNamePaginatedSpecification(
        Expression<Func<TData, string?>> nameSelector,
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) == false)
        {
            var normalizedSearch = searchTerm.Trim();
            Query.Search(nameSelector, $"%{normalizedSearch}%");
        }

        var normalizedPageNumber = pageNumber < 1 ? 1 : pageNumber;
        var normalizedPageSize = pageSize < 1 ? 10 : pageSize;
        Query.Skip((normalizedPageNumber - 1) * normalizedPageSize)
            .Take(normalizedPageSize);
    }
}
