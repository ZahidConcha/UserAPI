using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class puestoDepartamentoRelacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TPuestos_DepartamentosId",
                table: "TPuestos",
                column: "DepartamentosId");

            migrationBuilder.AddForeignKey(
                name: "FK_TPuestos_Departamentos_DepartamentosId",
                table: "TPuestos",
                column: "DepartamentosId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TPuestos_Departamentos_DepartamentosId",
                table: "TPuestos");

            migrationBuilder.DropIndex(
                name: "IX_TPuestos_DepartamentosId",
                table: "TPuestos");
        }
    }
}
