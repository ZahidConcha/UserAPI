using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class RemovedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_TSitios_SitiosId",
                table: "Departamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_TPuestos_Departamentos_DepartamentosId",
                table: "TPuestos");

            migrationBuilder.DropIndex(
                name: "IX_TPuestos_DepartamentosId",
                table: "TPuestos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_SitiosId",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "DepartamentosId",
                table: "TPuestos");

            migrationBuilder.DropColumn(
                name: "SitiosId",
                table: "Departamentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentosId",
                table: "TPuestos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SitiosId",
                table: "Departamentos",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TPuestos_DepartamentosId",
                table: "TPuestos",
                column: "DepartamentosId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TPuestos_Departamentos_DepartamentosId",
                table: "TPuestos",
                column: "DepartamentosId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
