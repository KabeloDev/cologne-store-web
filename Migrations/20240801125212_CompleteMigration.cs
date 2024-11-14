using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CologneStore.Migrations
{
    /// <inheritdoc />
    public partial class CompleteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CologneForId",
                table: "Colognes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Colognes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ColognesFor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CologneForName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColognesFor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colognes_CologneForId",
                table: "Colognes",
                column: "CologneForId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colognes_ColognesFor_CologneForId",
                table: "Colognes",
                column: "CologneForId",
                principalTable: "ColognesFor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colognes_ColognesFor_CologneForId",
                table: "Colognes");

            migrationBuilder.DropTable(
                name: "ColognesFor");

            migrationBuilder.DropIndex(
                name: "IX_Colognes_CologneForId",
                table: "Colognes");

            migrationBuilder.DropColumn(
                name: "CologneForId",
                table: "Colognes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Colognes");
        }
    }
}
