using casefile.application.DTOs.TemplateDossier;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IGetTemplateDossierForEdit
{
    Task<Result<UpdateTemplateDossierDto>> ExecuteAsync(Guid id);
}
