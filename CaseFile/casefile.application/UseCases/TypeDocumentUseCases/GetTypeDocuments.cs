using casefile.application.DTOs.TypeDocument;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using FluentResults;

namespace casefile.application.UseCases.TypeDocumentUseCases;

/// <summary>
/// Récupère la liste de tous les types de documents disponibles.
/// </summary>
public class GetTypeDocuments(ITypeDocumentRepository typeDocumentRepository) : IGetTypeDocuments
{
    public async Task<Result<IEnumerable<TypeDocumentDto>>> ExecuteAsync()
    {
        var result = await typeDocumentRepository.GetAllAsync();
        if (result.IsFailed)
            return Result.Fail<IEnumerable<TypeDocumentDto>>(result.Errors);

        var dtos = result.Value.Select(t => new TypeDocumentDto
        {
            Id = t.Id,
            Code = t.Code,
            Nom = t.Nom,
            ExtensionsPermises = t.ExtensionsPermises,
            DossierCibleParDefaut = t.DossierCibleParDefaut
        });

        return Result.Ok(dtos);
    }
}
