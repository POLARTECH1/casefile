using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using FluentResults;

namespace casefile.application.UseCases.ClientUseCases;

public class DeleteClient(IClientRepository clientRepository) : IDeleteClient
{
    public async Task<Result<bool>> ExecuteAsync(Guid id)
    {
        var result = await clientRepository.DeleteAsync(id);
        if (result.IsFailed)
        {
            return Result.Fail<bool>(
                new Error("Impossible de supprimer le client. Une erreur est survenue."));
        }

        return Result.Ok(true);
    }
}
