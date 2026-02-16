using Ardalis.Specification;
using System.Linq.Expressions;

namespace casefile.data.Specifications;

/// <summary>
/// Spécification permettant de filtrer des entités en fonction d'un critère de recherche sur un champ texte.
/// </summary>
/// <typeparam name="TData">
/// Type de l'entité sur laquelle la spécification s'applique.
/// </typeparam>
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
