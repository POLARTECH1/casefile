using casefile.application.DTOs.TemplateDossier;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetTemplateDossierItems
{
    Task<Result<IEnumerable<TemplateDossierItemDto>>> ExecuteAsync();
}
