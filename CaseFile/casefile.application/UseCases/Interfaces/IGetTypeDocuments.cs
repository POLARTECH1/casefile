using casefile.application.DTOs.TypeDocument;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetTypeDocuments
{
    Task<Result<IEnumerable<TypeDocumentDto>>> ExecuteAsync();
}
