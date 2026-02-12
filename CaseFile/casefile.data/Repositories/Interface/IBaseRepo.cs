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
    /// Retourne le nombre de ligne affectees.
    /// </summary>
    Task<Result<int>> UpdateAsync(TData data, bool saveChanges = true);

    /// <summary>
    /// Supprime une entite.
    /// Retourne le nombre de ligne affectees.
    /// </summary>
    Task<Result<int>> DeleteAsync(Guid id, bool saveChanges = true);

    /// <summary>
    /// Persiste les changements (async).
    /// Retourne le nombre de lignes affectees
    /// </summary>
    Task<Result<int>> SaveChangesAsync();

    /// <summary>
    /// Persiste les changements (sync).
    /// Retourne le nombre de ligne affectees
    /// </summary>
    Result<int> SaveChanges();

    /// <summary>
    /// Verifie l'existence d'une entite par identifiant.
    /// </summary>
    Task<Result<bool>> ExistsAsync(Guid id);

    /// <summary>
    /// Compte le nombre total d'entites.
    /// </summary>
    Task<Result<int>> CountAsync();
}