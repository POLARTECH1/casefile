using casefile.application.DTOs.TemplateDossier;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetTemplateDossierItem
{
    Task<Result<TemplateDossierItemDto>> ExecuteAsync(Guid id);
}
