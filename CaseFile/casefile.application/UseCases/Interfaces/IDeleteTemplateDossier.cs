using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IDeleteTemplateDossier
{
    /// <summary>
    /// Supprime un template de dossier.
    /// </summary>
    /// <param name="id">L'identifiant du template</param>
    /// <returns>Si le template a ete supprime ou non</returns>
    Task<Result<bool>> ExecuteAsync(Guid id);
}