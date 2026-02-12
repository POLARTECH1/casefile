using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
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
    /// Recupere toutes les entites correspondant a une specification.
    /// </summary>
    public virtual async Task<Result<IEnumerable<TData>>> GetAllAsync(ISpecification<TData> specification)
    {

        try
        {
            var query = ApplySpecification(specification);
            var entities = await query.ToListAsync();
            return Result.Ok<IEnumerable<TData>>(entities);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la recuperation des entites {EntityType} avec specification",
                typeof(TData).Name);
            return Result.Fail<IEnumerable<TData>>(
                $"Erreur lors de la recuperation des entites {typeof(TData).Name} avec specification: {ex.Message}");
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
                $"Erreur lors de la recuperation de l'entité {typeof(TData).Name} ({id}): {ex.Message}");
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
            return Result.Fail<TData>($"Erreur lors de l'ajout de l'entite {typeof(TData).Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// Met a jour une entite.
    /// </summary>
    public virtual async Task<Result<int>> UpdateAsync(TData data, bool saveChanges = true)
    {
        try
        {
            
            Set.Update(data);
            if (saveChanges)
            {
                return await SaveChangesAsync();
            }

            return Result.Ok(0);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la mise à jour d'une entite de type {EntityType}", typeof(TData).Name);
            return Result.Fail<int>($"Erreur lors de la mise a jour de l'entité {typeof(TData).Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// Supprime une entite par identifiant.
    /// </summary>
    public virtual async Task<Result<int>> DeleteAsync(Guid id, bool saveChanges = true)
    {
        try
        {
            var entityResult = await GetByIdAsync(id);
            if (entityResult.IsFailed)
            {
                return Result.Fail<int>(entityResult.Errors);
            }

            Set.Remove(entityResult.Value);

            if (saveChanges)
            {
                return await SaveChangesAsync();
            }

            return Result.Ok(0);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la suppression de l'entité {EntityType} avec l'identifiant {Id}",
                typeof(TData).Name, id);
            return Result.Fail<int>($"Erreur lors de la suppression de l'entité {typeof(TData).Name} ({id}): {ex.Message}");
        }
    }

    /// <summary>
    /// Persiste les changements (async).
    /// </summary>
    public async Task<Result<int>> SaveChangesAsync()
    {
        try
        {
            var affectedRow = await Context.SaveChangesAsync();
            return Result.Ok(affectedRow);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la persistence asynchrone des changements pour l'entité {EntityType}",
                typeof(TData).Name);
            return Result.Fail<int>($"Erreur lors de la persistence des changements: {ex.Message}");
        }
    }

    /// <summary>
    /// Persiste les changements (sync).
    /// </summary>
    public Result<int> SaveChanges()
    {
        try
        {
            var affectedRow = Context.SaveChanges();
            return Result.Ok(affectedRow);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors de la persistence synchrone des changements pour l'entité {EntityType}",
                typeof(TData).Name);
            return Result.Fail<int>($"Erreur lors de la persistence des changements: {ex.Message}");
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
                $"Erreur lors de la verification d'existence de l'entité {typeof(TData).Name} ({id}): {ex.Message}");
        }
    }

    public async Task<Result<int>> CountAsync(ISpecification<TData>? specification = null)
    {
        try
        {
            var query = specification is null
                ? Set.AsQueryable()
                : ApplySpecification(specification, true);
            var count = await query.CountAsync();
            return Result.Ok(count);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Erreur lors du comptage des entites de type {EntityType}", typeof(TData).Name);
            return Result.Fail<int>($"Erreur lors du comptage des entites {typeof(TData).Name}: {ex.Message}");
        }
    }

    private IQueryable<TData> ApplySpecification(ISpecification<TData> specification, bool evaluateCriteriaOnly = false)
    {
        return SpecificationEvaluator.Default.GetQuery(Set.AsQueryable(), specification, evaluateCriteriaOnly);
    }
}
