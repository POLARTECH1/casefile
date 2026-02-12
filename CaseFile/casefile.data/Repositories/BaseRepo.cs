using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace casefile.data.Repositories;

public class BaseRepo<TData> : IBaseRepo<TData> where TData : class
{
    protected readonly CaseFileContext Context;
    protected readonly DbSet<TData> Set;

    public BaseRepo(CaseFileContext context)
    {
        Context = context;
        Set = context.Set<TData>();
    }

    /// <summary>
    /// Recupere toutes les entites.
    /// </summary>
    public virtual async Task<IEnumerable<TData>> GetAllAsync()
    {
        return await Set.ToListAsync();
    }

    /// <summary>
    /// Recupere une entite par son identifiant.
    /// </summary>
    public virtual async Task<TData?> GetByIdAsync(Guid id)
    {
        return await Set.FindAsync(id);
    }

    /// <summary>
    /// Ajoute une entite.
    /// </summary>
    public virtual async Task<TData> AddAsync(TData data, bool saveChanges = true)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        await Set.AddAsync(data);

        if (saveChanges)
        {
            await Context.SaveChangesAsync();
        }

        return data;
    }

    /// <summary>
    /// Met a jour une entite.
    /// </summary>
    public virtual async Task UpdateAsync(TData data, bool saveChanges = true)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        Set.Update(data);

        if (saveChanges)
        {
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Supprime une entite par identifiant.
    /// </summary>
    public virtual async Task DeleteAsync(Guid id, bool saveChanges = true)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
        {
            return;
        }

        Set.Remove(entity);

        if (saveChanges)
        {
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Persiste les changements (async).
    /// </summary>
    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }

    /// <summary>
    /// Persiste les changements (sync).
    /// </summary>
    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    /// <summary>
    /// Verifie l'existence d'une entite par identifiant.
    /// </summary>
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await Set.FindAsync(id) is not null;
    }

    public async Task<int> CountAsync()
    {
        return await Set.CountAsync();
    }
    
}
