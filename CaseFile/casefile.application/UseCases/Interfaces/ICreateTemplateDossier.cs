using casefile.application.DTOs.TemplateDossier;
using FluentResults;

namespace casefile.application.UseCases.Interfaces;

public interface ICreateTemplateDossier
{
    Task<Result<TemplateDossierDto>> ExecuteAsync(CreateTemplateDossierDto createTemplateDossierDto);
}