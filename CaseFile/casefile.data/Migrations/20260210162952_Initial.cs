using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casefile.data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfilsEntreprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: true),
                    Adresse = table.Column<string>(type: "text", nullable: true),
                    LogoPath = table.Column<string>(type: "text", nullable: true),
                    Signature = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilsEntreprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReglesNommageDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Pattern = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReglesNommageDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchemasClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemasClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplatesDossier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatesDossier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    ExtensionsPermises = table.Column<string>(type: "text", nullable: true),
                    DossierCibleParDefaut = table.Column<string>(type: "text", nullable: true),
                    RegleNommageDocumentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypesDocument_ReglesNommageDocuments_RegleNommageDocumentId",
                        column: x => x.RegleNommageDocumentId,
                        principalTable: "ReglesNommageDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DefinitionsAttributs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cle = table.Column<string>(type: "text", nullable: false),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    EstRequis = table.Column<bool>(type: "boolean", nullable: false),
                    ValeurDefaut = table.Column<string>(type: "text", nullable: true),
                    SchemaClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefinitionsAttributs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefinitionsAttributs_SchemasClients_SchemaClientId",
                        column: x => x.SchemaClientId,
                        principalTable: "SchemasClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SchemaClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    TemplateDossierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_SchemasClients_SchemaClientId",
                        column: x => x.SchemaClientId,
                        principalTable: "SchemasClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clients_TemplatesDossier_TemplateDossierId",
                        column: x => x.TemplateDossierId,
                        principalTable: "TemplatesDossier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TemplatesCourriel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Sujet = table.Column<string>(type: "text", nullable: false),
                    Corps = table.Column<string>(type: "text", nullable: false),
                    TemplateDossierId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfilEntrepriseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatesCourriel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplatesCourriel_ProfilsEntreprise_ProfilEntrepriseId",
                        column: x => x.ProfilEntrepriseId,
                        principalTable: "ProfilsEntreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplatesCourriel_TemplatesDossier_TemplateDossierId",
                        column: x => x.TemplateDossierId,
                        principalTable: "TemplatesDossier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TemplatesDossierElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    IdParent = table.Column<Guid>(type: "uuid", nullable: true),
                    Ordre = table.Column<int>(type: "integer", nullable: false),
                    TemplateDossierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplatesDossierElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplatesDossierElements_TemplatesDossierElements_IdParent",
                        column: x => x.IdParent,
                        principalTable: "TemplatesDossierElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemplatesDossierElements_TemplatesDossier_TemplateDossierId",
                        column: x => x.TemplateDossierId,
                        principalTable: "TemplatesDossier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DossiersClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    CheminVirtuel = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossiersClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossiersClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValeursAttributsClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cle = table.Column<string>(type: "text", nullable: false),
                    Valeur = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeursAttributsClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValeursAttributsClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsAttendus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTypeDocument = table.Column<Guid>(type: "uuid", nullable: true),
                    TemplateDossierElementId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsAttendus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsAttendus_TemplatesDossierElements_TemplateDossierE~",
                        column: x => x.TemplateDossierElementId,
                        principalTable: "TemplatesDossierElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsAttendus_TypesDocument_IdTypeDocument",
                        column: x => x.IdTypeDocument,
                        principalTable: "TypesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CourrielsEnvoyes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    A = table.Column<string>(type: "text", nullable: false),
                    Sujet = table.Column<string>(type: "text", nullable: false),
                    EnvoyeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PiecesJointes = table.Column<string>(type: "text", nullable: false),
                    DossierClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourrielsEnvoyes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourrielsEnvoyes_DossiersClients_DossierClientId",
                        column: x => x.DossierClientId,
                        principalTable: "DossiersClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomOriginal = table.Column<string>(type: "text", nullable: false),
                    NomStandardise = table.Column<string>(type: "text", nullable: false),
                    CheminPhysique = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    AjouteLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TypeDocumentId = table.Column<Guid>(type: "uuid", nullable: true),
                    DossierClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsClients_DossiersClients_DossierClientId",
                        column: x => x.DossierClientId,
                        principalTable: "DossiersClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsClients_TypesDocument_TypeDocumentId",
                        column: x => x.TypeDocumentId,
                        principalTable: "TypesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SchemaClientId",
                table: "Clients",
                column: "SchemaClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TemplateDossierId",
                table: "Clients",
                column: "TemplateDossierId");

            migrationBuilder.CreateIndex(
                name: "IX_CourrielsEnvoyes_DossierClientId",
                table: "CourrielsEnvoyes",
                column: "DossierClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DefinitionsAttributs_Cle",
                table: "DefinitionsAttributs",
                column: "Cle");

            migrationBuilder.CreateIndex(
                name: "IX_DefinitionsAttributs_SchemaClientId",
                table: "DefinitionsAttributs",
                column: "SchemaClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsAttendus_IdTypeDocument",
                table: "DocumentsAttendus",
                column: "IdTypeDocument");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsAttendus_TemplateDossierElementId",
                table: "DocumentsAttendus",
                column: "TemplateDossierElementId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsClients_DossierClientId",
                table: "DocumentsClients",
                column: "DossierClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsClients_TypeDocumentId",
                table: "DocumentsClients",
                column: "TypeDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DossiersClients_ClientId",
                table: "DossiersClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesCourriel_ProfilEntrepriseId",
                table: "TemplatesCourriel",
                column: "ProfilEntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesCourriel_TemplateDossierId",
                table: "TemplatesCourriel",
                column: "TemplateDossierId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesDossierElements_IdParent",
                table: "TemplatesDossierElements",
                column: "IdParent");

            migrationBuilder.CreateIndex(
                name: "IX_TemplatesDossierElements_TemplateDossierId",
                table: "TemplatesDossierElements",
                column: "TemplateDossierId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesDocument_Code",
                table: "TypesDocument",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesDocument_RegleNommageDocumentId",
                table: "TypesDocument",
                column: "RegleNommageDocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValeursAttributsClients_Cle",
                table: "ValeursAttributsClients",
                column: "Cle");

            migrationBuilder.CreateIndex(
                name: "IX_ValeursAttributsClients_ClientId_Cle",
                table: "ValeursAttributsClients",
                columns: new[] { "ClientId", "Cle" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourrielsEnvoyes");

            migrationBuilder.DropTable(
                name: "DefinitionsAttributs");

            migrationBuilder.DropTable(
                name: "DocumentsAttendus");

            migrationBuilder.DropTable(
                name: "DocumentsClients");

            migrationBuilder.DropTable(
                name: "TemplatesCourriel");

            migrationBuilder.DropTable(
                name: "ValeursAttributsClients");

            migrationBuilder.DropTable(
                name: "TemplatesDossierElements");

            migrationBuilder.DropTable(
                name: "DossiersClients");

            migrationBuilder.DropTable(
                name: "TypesDocument");

            migrationBuilder.DropTable(
                name: "ProfilsEntreprise");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ReglesNommageDocuments");

            migrationBuilder.DropTable(
                name: "SchemasClients");

            migrationBuilder.DropTable(
                name: "TemplatesDossier");
        }
    }
}
