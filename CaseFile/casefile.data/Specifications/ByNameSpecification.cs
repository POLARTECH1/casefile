using Ardalis.Specification;
using System.Linq.Expressions;

namespace casefile.data.Specifications;

public class ByNameSpecification<TData> : Specification<TData> where TData : class
{
    public ByNameSpecification(Expression<Func<TData, string?>> nameSelector, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm) == false)
        {
            var normalizedSearch = searchTerm.Trim();
            Query.Search(nameSelector, $"%{normalizedSearch}%");
        }
    }
}
