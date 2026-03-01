using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using FluentResults;

namespace casefile.application.UseCases.SchemaClientUseCases;

/// <summary>
/// Classe responsable de la suppression d'un schéma client.
/// Cette classe implémente l'interface IDeleteSchemaClient.
/// </summary>
public class DeleteSchemaClient(ISchemaClientRepository schemaClientRepository) : IDeleteSchemaClient
{
    public async Task<Result<bool>> ExecuteAsync(Guid id)
    {
        var result = await schemaClientRepository.DeleteAsync(id);
        if (result.IsFailed)
        {
            return Result.Fail<bool>(
                new Error("Impossible de supprimer le schéma client. Une erreur est survenue."));
        }

        return Result.Ok(true);
    }
}
