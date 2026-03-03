using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using FluentResults;

namespace casefile.application.UseCases.TemplateDossierUseCases;

public class DeleteTemplateDossier(ITemplateDossierRepository templateDossierRepository) : IDeleteTemplateDossier
{
    /// <summary>
    /// Supprime un template de dossier.
    /// </summary>
    /// <param name="id">L'identifiant du template</param>
    /// <returns>Si le template a ete supprime ou non</returns>
    public async Task<Result<bool>> ExecuteAsync(Guid id)
    {
        var result = await templateDossierRepository.DeleteAsync(id);
        if (result.IsFailed)
        {
            return Result.Fail<bool>(
                new Error("Impossible de supprimer le template de dossier. Une erreur est survenue."));
        }

        return Result.Ok(true);
    }
}