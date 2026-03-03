using casefile.application.DTOs.TemplateDossier;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface IUpdateTemplateDossier
{
    Task<Result<TemplateDossierDto>> ExecuteAsync(UpdateTemplateDossierDto updateTemplateDossierDto);
}
