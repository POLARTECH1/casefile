using Ardalis.Specification;
using System.Linq.Expressions;

namespace casefile.data.Specifications;

/// <summary>
/// Spécification générique paginée par nom permettant de filtrer et paginer une collection d'entités.
/// </summary>
/// <typeparam name="TData">
/// Type de données à manipuler, qui doit être une classe.
/// </typeparam>
/// <remarks>
/// Cette spécification utilise un sélecteur d'expression pour déterminer la propriété de type chaîne de
/// caractères (généralement un nom) à filtrer, un terme de recherche optionnel, ainsi que les paramètres
/// de pagination (numéro de page et taille de page).
/// Si un terme de recherche est fourni, il applique un filtre basé sur une recherche partielle.
/// La pagination est appliquée en sautant un nombre défini d'éléments et en limitant le nombre total
/// d'éléments retournés.
/// </remarks>
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
