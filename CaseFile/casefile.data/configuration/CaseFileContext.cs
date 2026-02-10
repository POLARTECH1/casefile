using casefile.domain.model;
using Microsoft.EntityFrameworkCore;

namespace casefile.data.configuration;

public class CaseFileContext : DbContext
{
    public CaseFileContext() : base()
    {
    }

    public CaseFileContext(DbContextOptions<CaseFileContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<CourrielEnvoye> CourrielsEnvoyes => Set<CourrielEnvoye>();
    public DbSet<DefinitionAttribut> DefinitionsAttributs => Set<DefinitionAttribut>();
    public DbSet<DocumentAttendu> DocumentsAttendus => Set<DocumentAttendu>();
    public DbSet<DocumentClient> DocumentsClients => Set<DocumentClient>();
    public DbSet<DossierClient> DossiersClients => Set<DossierClient>();
    public DbSet<ProfilEntreprise> ProfilsEntreprise => Set<ProfilEntreprise>();
    public DbSet<RegleNommageDocument> ReglesNommageDocuments => Set<RegleNommageDocument>();
    public DbSet<SchemaClient> SchemasClients => Set<SchemaClient>();
    public DbSet<TemplateCourriel> TemplatesCourriel => Set<TemplateCourriel>();
    public DbSet<TemplateDossier> TemplatesDossier => Set<TemplateDossier>();
    public DbSet<TemplateDossierElement> TemplatesDossierElements => Set<TemplateDossierElement>();
    public DbSet<TypeDocument> TypesDocument => Set<TypeDocument>();
    public DbSet<ValeurAttributClient> ValeursAttributsClients => Set<ValeurAttributClient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            
        });
    }
}