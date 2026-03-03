using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casefile.data.Migrations
{
    /// <inheritdoc />
    public partial class AjouteProprieteEstRequisTableDocumentAttendus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "est_requis",
                table: "documents_attendus",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "est_requis",
                table: "documents_attendus");
        }
    }
}
