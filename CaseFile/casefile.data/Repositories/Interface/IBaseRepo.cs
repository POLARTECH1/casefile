using FluentResults;

namespace casefile.data.Repositories.Interface;

public interface IBaseRepo<TData> where TData : class
{
    /// <summary>
    /// Recupere toutes les entites.
    /// </summary>
    Task<Result<IEnumerable<TData>>> GetAllAsync();
    /// <summary>
    /// Recupere une entite par son identifiant.
    /// </summary>
    Task<Result<TData>> GetByIdAsync(Guid id);
    /// <summary>
    /// Ajoute une entite.
    /// </summary>
    Task<Result<TData>> AddAsync(TData data, bool saveChanges = true);
    /// <summary>
    /// Met a jour une entite.
    /// </summary>
    Task<Result> UpdateAsync(TData data, bool saveChanges = true);
    /// <summary>
    /// Supprime une entite.
    /// </summary>
    Task<Result> DeleteAsync(Guid id, bool saveChanges = true);
    /// <summary>
    /// Persiste les changements (async).
    /// </summary>
    Task<Result> SaveChangesAsync();
    /// <summary>
    /// Persiste les changements (sync).
    /// </summary>
    Result SaveChanges();
    /// <summary>
    /// Verifie l'existence d'une entite par identifiant.
    /// </summary>
    Task<Result<bool>> ExistsAsync(Guid id);
}
