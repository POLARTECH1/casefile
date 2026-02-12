using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
    public virtual async Task<Result<IEnumerable<TData>>> GetAllAsync()
    {
        try
        {
            var entities = await Set.ToListAsync();
            return Result.Ok<IEnumerable<TData>>(entities);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la recuperation de toutes les entites de type {EntityType}",
                typeof(TData).Name);
            return Result.Fail<IEnumerable<TData>>(
                $"Erreur lors de la recuperation des entites {typeof(TData).Name}.");
        }
    }

    /// <summary>
    /// Recupere une entite par son identifiant.
    /// </summary>
    public virtual async Task<Result<TData>> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await Set.FindAsync(id);
            if (entity is null)
            {
                return Result.Fail<TData>($"Entite {typeof(TData).Name} introuvable pour l'identifiant {id}.");
            }

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la recuperation de l'entité {EntityType} avec l'identifiant {Id}",
                typeof(TData).Name, id);
            return Result.Fail<TData>(
                $"Erreur lors de la recuperation de l'entité {typeof(TData).Name} ({id}).");
        }
    }

    /// <summary>
    /// Ajoute une entite.
    /// </summary>
    public virtual async Task<Result<TData>> AddAsync(TData data, bool saveChanges = true)
    {
        try
        {
            await Set.AddAsync(data);

            if (saveChanges)
            {
                var saveResult = await SaveChangesAsync();
                if (saveResult.IsFailed)
                {
                    return Result.Fail<TData>(saveResult.Errors);
                }
            }

            return Result.Ok(data);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de l'ajout d'une entite de type {EntityType}", typeof(TData).Name);
            return Result.Fail<TData>($"Erreur lors de l'ajout de l'entite {typeof(TData).Name}.");
        }
    }

    /// <summary>
    /// Met a jour une entite.
    /// </summary>
    public virtual async Task<Result> UpdateAsync(TData data, bool saveChanges = true)
    {
        try
        {
            Set.Update(data);

            if (saveChanges)
            {
                return await SaveChangesAsync();
            }

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la mise à jour d'une entite de type {EntityType}", typeof(TData).Name);
            return Result.Fail($"Erreur lors de la mise a jour de l'entité {typeof(TData).Name}.");
        }
    }

    /// <summary>
    /// Supprime une entite par identifiant.
    /// </summary>
    public virtual async Task<Result> DeleteAsync(Guid id, bool saveChanges = true)
    {
        try
        {
            var entityResult = await GetByIdAsync(id);
            if (entityResult.IsFailed)
            {
                return Result.Fail(entityResult.Errors);
            }

            Set.Remove(entityResult.Value);

            if (saveChanges)
            {
                return await SaveChangesAsync();
            }

            return Result.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la suppression de l'entité {EntityType} avec l'identifiant {Id}",
                typeof(TData).Name, id);
            return Result.Fail($"Erreur lors de la suppression de l'entité {typeof(TData).Name} ({id}).");
        }
    }

    /// <summary>
    /// Persiste les changements (async).
    /// </summary>
    public async Task<Result> SaveChangesAsync()
    {
        try
        {
            await Context.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la persistence asynchrone des changements pour l'entité {EntityType}",
                typeof(TData).Name);
            return Result.Fail($"Erreur lors de la persistence des changements.");
        }
    }

    /// <summary>
    /// Persiste les changements (sync).
    /// </summary>
    public Result SaveChanges()
    {
        try
        {
            Context.SaveChanges();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la persistence synchrone des changements pour l'entité {EntityType}",
                typeof(TData).Name);
            return Result.Fail($"Erreur lors de la persistence des changements.");
        }
    }

    /// <summary>
    /// Verifie l'existence d'une entite par identifiant.
    /// </summary>
    public async Task<Result<bool>> ExistsAsync(Guid id)
    {
        try
        {
            var exists = await Set.AsNoTracking()
                .AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
            return Result.Ok(exists);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la verification d'existence de l'entité {EntityType} avec l'identifiant {Id}",
                typeof(TData).Name, id);
            return Result.Fail<bool>(
                $"Erreur lors de la verification d'existence de l'entité {typeof(TData).Name} ({id}).");
        }
    }

    public async Task<Result<int>> CountAsync()
    {
        try
        {
            var count = await Set.CountAsync();
            return Result.Ok(count);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors du comptage des entites de type {EntityType}", typeof(TData).Name);
            return Result.Fail<int>($"Erreur lors du comptage des entites {typeof(TData).Name}.");
        }
    }
}