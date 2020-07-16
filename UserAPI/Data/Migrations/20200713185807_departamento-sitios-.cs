using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class departamentositios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SitiosId",
                table: "Departamentos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_SitiosId",
                table: "Departamentos",
                column: "SitiosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_TSitios_SitiosId",
                table: "Departamentos",
                column: "SitiosId",
                principalTable: "TSitios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_TSitios_SitiosId",
                table: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_SitiosId",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "SitiosId",
                table: "Departamentos");
        }
    }
}
