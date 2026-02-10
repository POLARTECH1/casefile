namespace casefile.data.Repositories.Interface;

public interface IBaseRepo<TData> where TData : class
{
    /// <summary>
    /// Recupere toutes les entites.
    /// </summary>
    Task<IEnumerable<TData>> GetAllAsync();
    /// <summary>
    /// Recupere une entite par son identifiant.
    /// </summary>
    Task<TData?> GetByIdAsync(Guid id);
    /// <summary>
    /// Ajoute une entite.
    /// </summary>
    Task<TData> AddAsync(TData data, bool saveChanges = true);
    /// <summary>
    /// Met a jour une entite.
    /// </summary>
    Task UpdateAsync(TData data, bool saveChanges = true);
    /// <summary>
    /// Supprime une entite.
    /// </summary>
    Task DeleteAsync(Guid id, bool saveChanges = true);
    /// <summary>
    /// Persiste les changements (async).
    /// </summary>
    Task SaveChangesAsync();
    /// <summary>
    /// Persiste les changements (sync).
    /// </summary>
    void SaveChanges();
    /// <summary>
    /// Verifie l'existence d'une entite par identifiant.
    /// </summary>
    Task<bool> ExistsAsync(Guid id);
}
