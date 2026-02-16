using AutoMapper;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.domain.model;
using FluentResults;
using FluentValidation;

namespace casefile.application.UseCases.TemplateDossierUseCases;

/// <summary>
/// Classe définissant le cas d'utilisation pour la création d'un TemplateDossier.
/// Permet de valider les données d'entrée, de mapper les objets nécessaires et de persister
/// les informations dans un repository.
/// </summary>
public class CreateTemplateDossier(
    ITemplateDossierRepository templateDossierRepository,
    IValidator<CreateTemplateDossierDto> validator,
    IMapper mapper) : ICreateTemplateDossier
{
    public async Task<Result<TemplateDossierDto>> ExecuteAsync(CreateTemplateDossierDto createTemplateDossierDto)
    {
        await validator.ValidateAndThrowAsync(createTemplateDossierDto);

        TemplateDossier templateDossier = new TemplateDossier()
        {
            Description = createTemplateDossierDto.Description,
            Nom = createTemplateDossierDto.Nom,
            Elements = createTemplateDossierDto.CreateTemplateDossierElementDtos.Select(e =>
                new TemplateDossierElement()
                {
                    Nom = e.Nom,
                    IdParent = e.IdParent,
                    Ordre = e.Ordre,
                    DocumentsAttendus = e.CreateDocumentAttendusDto.Select(d => new DocumentAttendu()
                    {
                        IdTypeDocument = d.IdTypeDocument
                    }).ToList()
                }).ToList()
        };

        var result = await templateDossierRepository.AddAsync(templateDossier);
        return result.ToResult<TemplateDossierDto>((_) => mapper.Map<TemplateDossierDto>(result.Value));
    }
}