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

#if DEBUG //Permet d'inclure cette méthode uniquement si l'application est en mode DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Vérifie si la configuration n'a pas été spécifiée par un fichier de configuration
        if (optionsBuilder.IsConfigured == false)
        {
            //Aucune configuration à partir d'un fichier de configuration
            //Option de base pour la migration
            string? chaineConnexion = Environment.GetEnvironmentVariable("MIGRATION_CONNECTION_STRING");
            //Vérifie si la variable n'est pas vide
            if (string.IsNullOrEmpty(chaineConnexion) == false)
            {
                //La variable n'est pas vide, la chaine de connexion est appliquée
                optionsBuilder.UseNpgsql(chaineConnexion).UseSnakeCaseNamingConvention();
            }
            else
            {
                //Il n'y a aucune chaine de connexion.
                throw new Exception(
                    "La variable MIGRATION_CONNECTION_STRING n'est pas spécifiée. Effectuez la commande suivante dans la Console du Gestionnaire de package : $env:MIGRATION_CONNECTION_STRING=\"[ma chaine de connexion]\" ");
            }
        }
    }
#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Prenom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Telephone).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(320);
            entity.Property(e => e.CreeLe).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasOne(e => e.SchemaClient)
                .WithMany()
                .HasForeignKey("SchemaClientId")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.TemplateDossier)
                .WithMany()
                .HasForeignKey("TemplateDossierId")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(e => e.ValeursAttributs)
                .WithOne()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Dossiers)
                .WithOne()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CourrielEnvoye>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.A).IsRequired().HasMaxLength(320);
            entity.Property(e => e.Sujet).IsRequired().HasMaxLength(200);
            entity.Property(e => e.EnvoyeLe).IsRequired();
            entity.Property(e => e.PiecesJointes).IsRequired().HasMaxLength(2000);
        });

        modelBuilder.Entity<DefinitionAttribut>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cle).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Libelle).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.EstRequis).IsRequired();
            entity.Property(e => e.ValeurDefaut).HasMaxLength(1000);

            entity.HasIndex(e => e.Cle);

            entity.HasOne<SchemaClient>()
                .WithMany(s => s.Definitions)
                .HasForeignKey("SchemaClientId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DocumentAttendu>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(e => e.TypeDocument)
                .WithMany()
                .HasForeignKey(e => e.IdTypeDocument)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<DocumentClient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NomOriginal).IsRequired().HasMaxLength(255);
            entity.Property(e => e.NomStandardise).IsRequired().HasMaxLength(255);
            entity.Property(e => e.CheminPhysique).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Extension).IsRequired().HasMaxLength(20);
            entity.Property(e => e.AjouteLe).IsRequired();

            entity.HasOne(e => e.TypeDocument)
                .WithMany()
                .HasForeignKey("TypeDocumentId")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<DossierClient>()
                .WithMany(d => d.Documents)
                .HasForeignKey("DossierClientId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DossierClient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.CheminVirtuel).IsRequired().HasMaxLength(500);

            entity.HasMany(e => e.Documents)
                .WithOne()
                .HasForeignKey("DossierClientId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Courriels)
                .WithOne()
                .HasForeignKey("DossierClientId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProfilEntreprise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(320);
            entity.Property(e => e.Telephone).HasMaxLength(50);
            entity.Property(e => e.Adresse).HasMaxLength(500);
            entity.Property(e => e.LogoPath).HasMaxLength(1000);
            entity.Property(e => e.Signature).HasMaxLength(4000);

            entity.HasMany(e => e.TemplatesCourriel)
                .WithOne()
                .HasForeignKey("ProfilEntrepriseId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RegleNommageDocument>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Pattern).IsRequired().HasMaxLength(500);
        });

        modelBuilder.Entity<SchemaClient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);

            entity.HasMany(e => e.Definitions)
                .WithOne()
                .HasForeignKey("SchemaClientId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TemplateCourriel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Sujet).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Corps).IsRequired().HasMaxLength(20000);

            entity.HasOne(e => e.TemplateDossier)
                .WithMany()
                .HasForeignKey("TemplateDossierId")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<ProfilEntreprise>()
                .WithMany(p => p.TemplatesCourriel)
                .HasForeignKey("ProfilEntrepriseId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TemplateDossier>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(2000);

            entity.HasMany(e => e.Elements)
                .WithOne()
                .HasForeignKey("TemplateDossierId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TemplateDossierElement>(entity =>
        {
            entity.ToTable(tb => tb.HasCheckConstraint("CK_template_dossier_element_ordre_non_negatif", "Ordre >= 0"));
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Ordre).IsRequired();

            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Enfants)
                .HasForeignKey(e => e.IdParent)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.DocumentsAttendus)
                .WithOne()
                .HasForeignKey("TemplateDossierElementId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TypeDocument>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(150);
            entity.Property(e => e.ExtensionsPermises).HasMaxLength(500);
            entity.Property(e => e.DossierCibleParDefaut).HasMaxLength(500);

            entity.HasIndex(e => e.Code).IsUnique();

            entity.HasOne(e => e.RegleNommage)
                .WithOne()
                .HasForeignKey<TypeDocument>("RegleNommageDocumentId")
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ValeurAttributClient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cle).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Valeur).IsRequired().HasMaxLength(4000);

            entity.HasIndex(e => e.Cle);
            entity.HasIndex("ClientId", "Cle").IsUnique();

            entity.HasOne<Client>()
                .WithMany(c => c.ValeursAttributs)
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
