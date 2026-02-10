using System;
using System.Collections.Generic;
using Bogus;
using casefile.data.configuration;
using casefile.domain.model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace casefile.desktop.Tools;

/// <summary>
/// Represente une classe responsable de seeder des donnees dans la base de donnees.
/// </summary>
public static class Seeder
{
    public static void Seed(CaseFileContext dbContext)
    {
        if (dbContext.Clients.Any() ||
            dbContext.SchemasClients.Any() ||
            dbContext.TypesDocument.Any() ||
            dbContext.ProfilsEntreprise.Any())
        {
            return;
        }

        Randomizer.Seed = new Random(1245);

        var reglesNommage = SeedReglesNommageDocuments();
        var typesDocument = SeedTypesDocument(reglesNommage);
        var templatesDossier = SeedTemplatesDossier(typesDocument);
        var schemas = SeedSchemasClients();
        var profils = SeedProfilsEntreprise(templatesDossier);
        var clients = SeedClients(schemas, templatesDossier, typesDocument);

        dbContext.ReglesNommageDocuments.AddRange(reglesNommage);
        dbContext.TypesDocument.AddRange(typesDocument);
        dbContext.TemplatesDossier.AddRange(templatesDossier);
        dbContext.SchemasClients.AddRange(schemas);
        dbContext.ProfilsEntreprise.AddRange(profils);
        dbContext.Clients.AddRange(clients);

        NormalizeDateTimes(dbContext);
        dbContext.SaveChanges();
    }
    
    private static List<RegleNommageDocument> SeedReglesNommageDocuments()
    {
        return new List<RegleNommageDocument>
        {
            new() { Id = Guid.NewGuid(), Pattern = "{client}_{type}_{date:yyyyMMdd}" },
            new() { Id = Guid.NewGuid(), Pattern = "{type}_{client}_{seq:000}" },
            new() { Id = Guid.NewGuid(), Pattern = "{date:yyyyMMdd}_{type}_{client}" },
            new() { Id = Guid.NewGuid(), Pattern = "{client}_{type}_{date:yyyyMMddHHmm}" },
            new() { Id = Guid.NewGuid(), Pattern = "{type}_{date:yyyy-MM}_{client}" }
        };
    }

    private static List<TypeDocument> SeedTypesDocument(IReadOnlyList<RegleNommageDocument> regles)
    {
        var types = new List<TypeDocument>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Code = "ID",
                Nom = "Pièce d'identité",
                ExtensionsPermises = ".pdf;.jpg;.png",
                DossierCibleParDefaut = "Identite",
                RegleNommage = regles[0]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Code = "DOM",
                Nom = "Preuve de domicile",
                ExtensionsPermises = ".pdf;.jpg;.png",
                DossierCibleParDefaut = "Justificatifs",
                RegleNommage = regles[1]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Code = "CONTRAT",
                Nom = "Contrat",
                ExtensionsPermises = ".pdf;.docx",
                DossierCibleParDefaut = "Contrats",
                RegleNommage = regles[2]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Code = "IMPOT",
                Nom = "Avis d'imposition",
                ExtensionsPermises = ".pdf",
                DossierCibleParDefaut = "Fiscalite",
                RegleNommage = regles[3]
            },
            new()
            {
                Id = Guid.NewGuid(),
                Code = "RIB",
                Nom = "RIB",
                ExtensionsPermises = ".pdf;.png",
                DossierCibleParDefaut = "Banque",
                RegleNommage = regles[4]
            }
        };

        return types;
    }

    private static List<TemplateDossier> SeedTemplatesDossier(IReadOnlyList<TypeDocument> typesDocument)
    {
        var templates = new List<TemplateDossier>();
        var faker = new Faker("fr");

        for (var i = 0; i < 3; i++)
        {
            var template = new TemplateDossier
            {
                Id = Guid.NewGuid(),
                Nom = $"Template {faker.Company.CatchPhrase()}",
                Description = faker.Lorem.Sentence()
            };

            var parentCount = faker.Random.Int(2, 4);
            for (var p = 0; p < parentCount; p++)
            {
                var parent = new TemplateDossierElement
                {
                    Id = Guid.NewGuid(),
                    Nom = faker.Commerce.Department(),
                    Ordre = p + 1
                };

                var childCount = faker.Random.Int(1, 3);
                for (var c = 0; c < childCount; c++)
                {
                    var child = new TemplateDossierElement
                    {
                        Id = Guid.NewGuid(),
                        Nom = faker.Commerce.ProductName(),
                        Ordre = c + 1,
                        Parent = parent
                    };

                    var attenduCount = faker.Random.Int(1, 2);
                    for (var d = 0; d < attenduCount; d++)
                    {
                        var typeDoc = PickRandom(typesDocument, faker);
                        child.DocumentsAttendus.Add(new DocumentAttendu
                        {
                            Id = Guid.NewGuid(),
                            IdTypeDocument = typeDoc.Id,
                            TypeDocument = typeDoc
                        });
                    }

                    parent.Enfants.Add(child);
                }

                template.Elements.Add(parent);
            }

            templates.Add(template);
        }

        return templates;
    }

    private static List<SchemaClient> SeedSchemasClients()
    {
        return new List<SchemaClient>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Nom = "Particulier",
                Definitions =
                {
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "date_naissance",
                        Libelle = "Date de naissance",
                        Type = TypeAttribut.Date,
                        EstRequis = true
                    },
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "statut_civil",
                        Libelle = "Statut civil",
                        Type = TypeAttribut.Liste,
                        EstRequis = true,
                        ValeurDefaut = "Celibataire"
                    },
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "revenu_annuel",
                        Libelle = "Revenu annuel",
                        Type = TypeAttribut.Nombre,
                        EstRequis = false
                    }
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nom = "Entreprise",
                Definitions =
                {
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "siren",
                        Libelle = "SIREN",
                        Type = TypeAttribut.Texte,
                        EstRequis = true
                    },
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "date_creation",
                        Libelle = "Date de création",
                        Type = TypeAttribut.Date,
                        EstRequis = true
                    },
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "effectif",
                        Libelle = "Effectif",
                        Type = TypeAttribut.Nombre,
                        EstRequis = false
                    }
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nom = "Immobilier",
                Definitions =
                {
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "type_bien",
                        Libelle = "Type de bien",
                        Type = TypeAttribut.Liste,
                        EstRequis = true,
                        ValeurDefaut = "Appartement"
                    },
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "surface",
                        Libelle = "Surface",
                        Type = TypeAttribut.Nombre,
                        EstRequis = true
                    },
                    new DefinitionAttribut
                    {
                        Id = Guid.NewGuid(),
                        Cle = "date_signature",
                        Libelle = "Date de signature",
                        Type = TypeAttribut.Date,
                        EstRequis = false
                    }
                }
            }
        };
    }

    private static List<ProfilEntreprise> SeedProfilsEntreprise(IReadOnlyList<TemplateDossier> templatesDossier)
    {
        var faker = new Faker("fr");
        var profils = new List<ProfilEntreprise>();

        for (var i = 0; i < 2; i++)
        {
            var profil = new ProfilEntreprise
            {
                Id = Guid.NewGuid(),
                Nom = faker.Company.CompanyName(),
                Email = faker.Internet.Email(),
                Telephone = faker.Phone.PhoneNumber("###-###-####"),
                Adresse = faker.Address.FullAddress(),
                LogoPath = $"logos/{faker.Random.Guid():N}.png",
                Signature = faker.Name.FullName()
            };

            var templateCount = faker.Random.Int(2, 4);
            for (var t = 0; t < templateCount; t++)
            {
                var templateDossier = PickRandom(templatesDossier, faker);
                profil.TemplatesCourriel.Add(new TemplateCourriel
                {
                    Id = Guid.NewGuid(),
                    Nom = $"Relance {t + 1}",
                    Sujet = faker.Company.CatchPhrase(),
                    Corps = faker.Lorem.Paragraphs(2),
                    TemplateDossier = templateDossier
                });
            }

            profils.Add(profil);
        }

        return profils;
    }

    private static List<Client> SeedClients(
        IReadOnlyList<SchemaClient> schemas,
        IReadOnlyList<TemplateDossier> templatesDossier,
        IReadOnlyList<TypeDocument> typesDocument)
    {
        const int clientsCount = 80;
        var faker = new Faker("fr");
        var clients = new List<Client>(clientsCount);

        for (var i = 0; i < clientsCount; i++)
        {
            var prenom = faker.Name.FirstName();
            var nom = faker.Name.LastName();
            var schema = PickRandom(schemas, faker);
            var templateDossier = PickRandom(templatesDossier, faker);

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Prenom = prenom,
                Nom = nom,
                Telephone = faker.Phone.PhoneNumber("###-###-####"),
                Email = faker.Internet.Email($"{prenom}.{nom}.{faker.UniqueIndex}", nom, "example.com")
                    .ToLowerInvariant(),
                CreeLe = ToUtc(faker.Date.Past(3)),
                SchemaClient = schema,
                TemplateDossier = templateDossier
            };

            foreach (var definition in schema.Definitions)
            {
                client.ValeursAttributs.Add(new ValeurAttributClient
                {
                    Id = Guid.NewGuid(),
                    Cle = definition.Cle,
                    Valeur = BuildValeurAttribut(definition, faker)
                });
            }

            var dossierCount = faker.Random.Int(2, 5);
            for (var d = 0; d < dossierCount; d++)
            {
                var dossier = new DossierClient
                {
                    Id = Guid.NewGuid(),
                    Nom = $"{faker.Commerce.Department()} {d + 1}",
                    CheminVirtuel = $"/clients/{client.Id:N}/dossier-{d + 1}"
                };

                var docsCount = faker.Random.Int(2, 6);
                for (var docIndex = 0; docIndex < docsCount; docIndex++)
                {
                    var typeDoc = PickRandom(typesDocument, faker);
                    var dateDoc = ToUtc(faker.Date.Past(2));
                    var extension = PickExtension(typeDoc, faker);
                    var standardName = BuildNomStandardise(client, typeDoc, dateDoc, docIndex + 1, extension);

                    dossier.Documents.Add(new DocumentClient
                    {
                        Id = Guid.NewGuid(),
                        NomOriginal = faker.System.FileName(extension.TrimStart('.')),
                        NomStandardise = standardName,
                        CheminPhysique = $"/data/{client.Id:N}/{standardName}",
                        Extension = extension,
                        AjouteLe = dateDoc,
                        TypeDocument = typeDoc
                    });
                }

                var courrielCount = faker.Random.Int(0, 3);
                for (var c = 0; c < courrielCount; c++)
                {
                    dossier.Courriels.Add(new CourrielEnvoye
                    {
                        Id = Guid.NewGuid(),
                        A = client.Email,
                        Sujet = faker.Company.CatchPhrase(),
                        EnvoyeLe = ToUtc(faker.Date.Recent(120)),
                        PiecesJointes = BuildPiecesJointes(dossier.Documents, faker)
                    });
                }

                client.Dossiers.Add(dossier);
            }

            clients.Add(client);
        }

        return clients;
    }

    private static string BuildValeurAttribut(DefinitionAttribut definition, Faker faker)
    {
        return definition.Type switch
        {
            TypeAttribut.Texte => faker.Lorem.Word(),
            TypeAttribut.Nombre => faker.Random.Int(1, 500).ToString(),
            TypeAttribut.Date => faker.Date.Past(40).ToString("yyyy-MM-dd"),
            TypeAttribut.Booleen => faker.Random.Bool() ? "true" : "false",
            TypeAttribut.Liste => faker.PickRandom(new[] { "Celibataire", "Marie", "Pacs", "Appartement", "Maison", "Local" }),
            _ => definition.ValeurDefaut ?? string.Empty
        };
    }

    private static string PickExtension(TypeDocument typeDocument, Faker faker)
    {
        if (string.IsNullOrWhiteSpace(typeDocument.ExtensionsPermises))
        {
            return faker.PickRandom(new[] { ".pdf", ".jpg", ".png", ".docx" });
        }

        var options = typeDocument.ExtensionsPermises
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return options.Length == 0 ? ".pdf" : faker.PickRandom(options);
    }

    private static string BuildNomStandardise(Client client, TypeDocument typeDocument, DateTime dateDoc, int sequence, string extension)
    {
        var safeNom = client.Nom.Replace(' ', '-').ToLowerInvariant();
        var safePrenom = client.Prenom.Replace(' ', '-').ToLowerInvariant();
        return $"{typeDocument.Code}_{safePrenom}_{safeNom}_{dateDoc:yyyyMMdd}_{sequence:000}{extension}";
    }

    private static string BuildPiecesJointes(ICollection<DocumentClient> documents, Faker faker)
    {
        if (documents.Count == 0)
        {
            return string.Empty;
        }

        var take = faker.Random.Int(1, Math.Min(3, documents.Count));
        var selection = faker.Random.ListItems(documents.ToList(), take);
        return string.Join(';', selection.Select(doc => doc.NomStandardise));
    }

    private static DateTime ToUtc(DateTime value)
    {
        if (value.Kind == DateTimeKind.Utc)
        {
            return value;
        }

        if (value.Kind == DateTimeKind.Local)
        {
            return value.ToUniversalTime();
        }

        return DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private static void NormalizeDateTimes(DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(DateTime))
                {
                    if (property.CurrentValue is DateTime value)
                    {
                        property.CurrentValue = ToUtc(value);
                    }
                }
                else if (property.Metadata.ClrType == typeof(DateTime?))
                {
                    if (property.CurrentValue is DateTime value)
                    {
                        property.CurrentValue = ToUtc(value);
                    }
                }
            }
        }
    }

    private static T PickRandom<T>(IReadOnlyList<T> items, Faker faker)
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("Impossible de choisir un element dans une liste vide.");
        }

        return items[faker.Random.Int(0, items.Count - 1)];
    }
}
