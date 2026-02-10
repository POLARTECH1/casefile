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
                name: "profils_entreprise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    telephone = table.Column<string>(type: "text", nullable: true),
                    adresse = table.Column<string>(type: "text", nullable: true),
                    logo_path = table.Column<string>(type: "text", nullable: true),
                    signature = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profils_entreprise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regles_nommage_documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    pattern = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regles_nommage_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "schemas_clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schemas_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "templates_dossier",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_templates_dossier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "types_document",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    extensions_permises = table.Column<string>(type: "text", nullable: true),
                    dossier_cible_par_defaut = table.Column<string>(type: "text", nullable: true),
                    regle_nommage_document_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_types_document", x => x.id);
                    table.ForeignKey(
                        name: "fk_types_document_regles_nommage_documents_regle_nommage_docum",
                        column: x => x.regle_nommage_document_id,
                        principalTable: "regles_nommage_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "definitions_attributs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cle = table.Column<string>(type: "text", nullable: false),
                    libelle = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    est_requis = table.Column<bool>(type: "boolean", nullable: false),
                    valeur_defaut = table.Column<string>(type: "text", nullable: true),
                    schema_client_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_definitions_attributs", x => x.id);
                    table.ForeignKey(
                        name: "fk_definitions_attributs_schemas_clients_schema_client_id",
                        column: x => x.schema_client_id,
                        principalTable: "schemas_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    prenom = table.Column<string>(type: "text", nullable: false),
                    telephone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    cree_le = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    schema_client_id = table.Column<Guid>(type: "uuid", nullable: true),
                    template_dossier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_schemas_clients_schema_client_id",
                        column: x => x.schema_client_id,
                        principalTable: "schemas_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_clients_templates_dossier_template_dossier_id",
                        column: x => x.template_dossier_id,
                        principalTable: "templates_dossier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "templates_courriel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    sujet = table.Column<string>(type: "text", nullable: false),
                    corps = table.Column<string>(type: "text", nullable: false),
                    template_dossier_id = table.Column<Guid>(type: "uuid", nullable: true),
                    profil_entreprise_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_templates_courriel", x => x.id);
                    table.ForeignKey(
                        name: "fk_templates_courriel_profils_entreprise_profil_entreprise_id",
                        column: x => x.profil_entreprise_id,
                        principalTable: "profils_entreprise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_templates_courriel_templates_dossier_template_dossier_id",
                        column: x => x.template_dossier_id,
                        principalTable: "templates_dossier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "templates_dossier_elements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    id_parent = table.Column<Guid>(type: "uuid", nullable: true),
                    ordre = table.Column<int>(type: "integer", nullable: false),
                    template_dossier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_templates_dossier_elements", x => x.id);
                    table.ForeignKey(
                        name: "fk_templates_dossier_elements_templates_dossier_elements_id_pa",
                        column: x => x.id_parent,
                        principalTable: "templates_dossier_elements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_templates_dossier_elements_templates_dossier_template_dossi",
                        column: x => x.template_dossier_id,
                        principalTable: "templates_dossier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dossiers_clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom = table.Column<string>(type: "text", nullable: false),
                    chemin_virtuel = table.Column<string>(type: "text", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dossiers_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_dossiers_clients_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "valeurs_attributs_clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cle = table.Column<string>(type: "text", nullable: false),
                    valeur = table.Column<string>(type: "text", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_valeurs_attributs_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_valeurs_attributs_clients_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents_attendus",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_type_document = table.Column<Guid>(type: "uuid", nullable: true),
                    template_dossier_element_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents_attendus", x => x.id);
                    table.ForeignKey(
                        name: "fk_documents_attendus_templates_dossier_elements_template_doss",
                        column: x => x.template_dossier_element_id,
                        principalTable: "templates_dossier_elements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_documents_attendus_types_document_id_type_document",
                        column: x => x.id_type_document,
                        principalTable: "types_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "courriels_envoyes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    a = table.Column<string>(type: "text", nullable: false),
                    sujet = table.Column<string>(type: "text", nullable: false),
                    envoye_le = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pieces_jointes = table.Column<string>(type: "text", nullable: false),
                    dossier_client_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courriels_envoyes", x => x.id);
                    table.ForeignKey(
                        name: "fk_courriels_envoyes_dossiers_clients_dossier_client_id",
                        column: x => x.dossier_client_id,
                        principalTable: "dossiers_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents_clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nom_original = table.Column<string>(type: "text", nullable: false),
                    nom_standardise = table.Column<string>(type: "text", nullable: false),
                    chemin_physique = table.Column<string>(type: "text", nullable: false),
                    extension = table.Column<string>(type: "text", nullable: false),
                    ajoute_le = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type_document_id = table.Column<Guid>(type: "uuid", nullable: true),
                    dossier_client_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_documents_clients_dossiers_clients_dossier_client_id",
                        column: x => x.dossier_client_id,
                        principalTable: "dossiers_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_documents_clients_types_document_type_document_id",
                        column: x => x.type_document_id,
                        principalTable: "types_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_clients_email",
                table: "clients",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_schema_client_id",
                table: "clients",
                column: "schema_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_template_dossier_id",
                table: "clients",
                column: "template_dossier_id");

            migrationBuilder.CreateIndex(
                name: "ix_courriels_envoyes_dossier_client_id",
                table: "courriels_envoyes",
                column: "dossier_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_definitions_attributs_cle",
                table: "definitions_attributs",
                column: "cle");

            migrationBuilder.CreateIndex(
                name: "ix_definitions_attributs_schema_client_id",
                table: "definitions_attributs",
                column: "schema_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_documents_attendus_id_type_document",
                table: "documents_attendus",
                column: "id_type_document");

            migrationBuilder.CreateIndex(
                name: "ix_documents_attendus_template_dossier_element_id",
                table: "documents_attendus",
                column: "template_dossier_element_id");

            migrationBuilder.CreateIndex(
                name: "ix_documents_clients_dossier_client_id",
                table: "documents_clients",
                column: "dossier_client_id");

            migrationBuilder.CreateIndex(
                name: "ix_documents_clients_type_document_id",
                table: "documents_clients",
                column: "type_document_id");

            migrationBuilder.CreateIndex(
                name: "ix_dossiers_clients_client_id",
                table: "dossiers_clients",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_templates_courriel_profil_entreprise_id",
                table: "templates_courriel",
                column: "profil_entreprise_id");

            migrationBuilder.CreateIndex(
                name: "ix_templates_courriel_template_dossier_id",
                table: "templates_courriel",
                column: "template_dossier_id");

            migrationBuilder.CreateIndex(
                name: "ix_templates_dossier_elements_id_parent",
                table: "templates_dossier_elements",
                column: "id_parent");

            migrationBuilder.CreateIndex(
                name: "ix_templates_dossier_elements_template_dossier_id",
                table: "templates_dossier_elements",
                column: "template_dossier_id");

            migrationBuilder.CreateIndex(
                name: "ix_types_document_code",
                table: "types_document",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_types_document_regle_nommage_document_id",
                table: "types_document",
                column: "regle_nommage_document_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_valeurs_attributs_clients_cle",
                table: "valeurs_attributs_clients",
                column: "cle");

            migrationBuilder.CreateIndex(
                name: "ix_valeurs_attributs_clients_client_id_cle",
                table: "valeurs_attributs_clients",
                columns: new[] { "client_id", "cle" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courriels_envoyes");

            migrationBuilder.DropTable(
                name: "definitions_attributs");

            migrationBuilder.DropTable(
                name: "documents_attendus");

            migrationBuilder.DropTable(
                name: "documents_clients");

            migrationBuilder.DropTable(
                name: "templates_courriel");

            migrationBuilder.DropTable(
                name: "valeurs_attributs_clients");

            migrationBuilder.DropTable(
                name: "templates_dossier_elements");

            migrationBuilder.DropTable(
                name: "dossiers_clients");

            migrationBuilder.DropTable(
                name: "types_document");

            migrationBuilder.DropTable(
                name: "profils_entreprise");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "regles_nommage_documents");

            migrationBuilder.DropTable(
                name: "schemas_clients");

            migrationBuilder.DropTable(
                name: "templates_dossier");
        }
    }
}
