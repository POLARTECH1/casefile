using AutoMapper;
using casefile.application.DTOs.Client;
using casefile.application.DTOs.CourrielEnvoye;
using casefile.application.DTOs.DefinitionAttribut;
using casefile.application.DTOs.DocumentAttendu;
using casefile.application.DTOs.DocumentClient;
using casefile.application.DTOs.DossierClient;
using casefile.application.DTOs.ProfilEntreprise;
using casefile.application.DTOs.RegleNommageDocument;
using casefile.application.DTOs.SchemaClient;
using casefile.application.DTOs.TemplateCourriel;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.DTOs.TemplateDossierElement;
using casefile.application.DTOs.TypeDocument;
using casefile.application.DTOs.ValeurAttributClient;
using casefile.domain.model;

namespace casefile.application.Mapping;

/// <summary>
/// MapperConfig est une classe de configuration pour AutoMapper utilisée pour définir
/// les mappings entre les entités du domaine et leurs Data Transfer Objects (DTOs).
/// Elle hérite de la classe Profile et regroupe les définitions des transformations,
/// permettant la conversion bidirectionnelle entre les objets.
/// </summary>
/// <remarks>
/// Cette classe inclut des mappages pour de nombreuses entités et DTOs, tels que:
/// - Client et ClientDto
/// - SchemaClient et SchemaClientDto
/// - DefinitionAttribut et DefinitionAttributDto
/// - Et d'autres entités et DTOs relatifs aux fonctionnalités du domaine.
/// Les mappages incluent également des traitements pour la création et la mise à jour
/// des entités.
/// </remarks>
/// <example>
/// Utilisé dans le cadre du service AutoMapper pour automatiser la transformation
/// des objets entre le modèle de domaine et les objets de transfert de données.
/// Aucun exemple explicite fourni dans cette documentation.
/// </example>
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<SchemaClient, SchemaClientDto>().ReverseMap();
        CreateMap<DefinitionAttribut, DefinitionAttributDto>().ReverseMap();
        CreateMap<ValeurAttributClient, ValeurAttributClientDto>().ReverseMap();
        CreateMap<DocumentAttendu, DocumentAttenduDto>().ReverseMap();
        CreateMap<DocumentClient, DocumentClientDto>().ReverseMap();
        CreateMap<CourrielEnvoye, CourrielEnvoyeDto>().ReverseMap();
        CreateMap<ProfilEntreprise, ProfilEntrepriseDto>().ReverseMap();
        CreateMap<TemplateCourriel, TemplateCourrielDto>().ReverseMap();
        CreateMap<TemplateDossier, TemplateDossierDto>().ReverseMap();
        CreateMap<TypeDocument, TypeDocumentDto>().ReverseMap();
        CreateMap<TemplateDossierElement, TemplateDossierElementDto>().ReverseMap();
        CreateMap<RegleNommageDocument, RegleNommageDocumentDto>().ReverseMap();
        CreateMap<UpdateClientDto, Client>().ReverseMap();
        CreateMap<CreateDocumentAttenduDto, DocumentAttendu>().ReverseMap();
        CreateMap<CreateDocumentClientDto, DocumentClient>().ReverseMap();
        CreateMap<CreateCourrielEnvoyeDto, CourrielEnvoye>().ReverseMap();
        CreateMap<CreateProfilEntrepriseDto, ProfilEntreprise>().ReverseMap();
        CreateMap<CreateTemplateCourrielDto, TemplateCourriel>().ReverseMap();
        CreateMap<CreateTemplateDossierDto, TemplateDossier>().ReverseMap();
        CreateMap<CreateTemplateDossierElementDto, TemplateDossierElement>().ReverseMap();
        CreateMap<CreateSchemaClientDto, SchemaClient>().ReverseMap();
        CreateMap<CreateDefinitionAttributDto, DefinitionAttribut>().ReverseMap();
        CreateMap<CreateValeurAttributClientDto, ValeurAttributClient>().ReverseMap();
        CreateMap<CreateTypeDocumentDto, TypeDocument>().ReverseMap();
        CreateMap<UpdateTemplateDossierDto, TemplateDossier>().ReverseMap();
        CreateMap<UpdateTemplateDossierElementDto, TemplateDossierElement>().ReverseMap();
        CreateMap<UpdateProfilEntrepriseDto, ProfilEntreprise>().ReverseMap();
        CreateMap<UpdateSchemaClientDto, SchemaClient>().ReverseMap();
        CreateMap<UpdateDefinitionAttributDto, DefinitionAttribut>().ReverseMap();
        CreateMap<UpdateValeurAttributClientDto, ValeurAttributClient>().ReverseMap();
        CreateMap<UpdateTypeDocumentDto, TypeDocument>().ReverseMap();
        CreateMap<UpdateDocumentAttenduDto, DocumentAttendu>().ReverseMap();
        CreateMap<UpdateDocumentClientDto, DocumentClient>().ReverseMap();
        CreateMap<UpdateCourrielEnvoyeDto, CourrielEnvoye>().ReverseMap();
        CreateMap<CreateRegleNommageDocumentDto, RegleNommageDocument>().ReverseMap();
        CreateMap<UpdateRegleNommageDocumentDto, RegleNommageDocument>().ReverseMap();
        CreateMap<DossierClient, DossierClientDto>().ReverseMap();
        CreateMap<DossierClient, CreateDossierClientDto>().ReverseMap();
        CreateMap<DossierClient, UpdateDossierClientDto>().ReverseMap();
    }
}