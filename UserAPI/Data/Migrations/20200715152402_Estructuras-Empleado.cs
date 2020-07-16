using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class EstructurasEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadoEstructura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estructura",
                table: "Estructura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departamentos",
                table: "Departamentos");

            migrationBuilder.RenameTable(
                name: "Estructura",
                newName: "TEstructuras");

            migrationBuilder.RenameTable(
                name: "Departamentos",
                newName: "TDepartamentos");

            migrationBuilder.AddColumn<int>(
                name: "EstructuraId",
                table: "TEmpleados",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "TEstructuras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "TEstructuras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuestoId",
                table: "TEstructuras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SitioId",
                table: "TEstructuras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEstructuras",
                table: "TEstructuras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TDepartamentos",
                table: "TDepartamentos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TEstructuras_DepartamentoId",
                table: "TEstructuras",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TEstructuras_EmpleadoId",
                table: "TEstructuras",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_TEstructuras_PuestoId",
                table: "TEstructuras",
                column: "PuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_TEstructuras_SitioId",
                table: "TEstructuras",
                column: "SitioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TDepartamentos_DepartamentoId",
                table: "TEstructuras",
                column: "DepartamentoId",
                principalTable: "TDepartamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras",
                column: "EmpleadoId",
                principalTable: "TEmpleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TPuestos_PuestoId",
                table: "TEstructuras",
                column: "PuestoId",
                principalTable: "TPuestos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TSitios_SitioId",
                table: "TEstructuras",
                column: "SitioId",
                principalTable: "TSitios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TDepartamentos_DepartamentoId",
                table: "TEstructuras");

            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras");

            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TPuestos_PuestoId",
                table: "TEstructuras");

            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TSitios_SitioId",
                table: "TEstructuras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEstructuras",
                table: "TEstructuras");

            migrationBuilder.DropIndex(
                name: "IX_TEstructuras_DepartamentoId",
                table: "TEstructuras");

            migrationBuilder.DropIndex(
                name: "IX_TEstructuras_EmpleadoId",
                table: "TEstructuras");

            migrationBuilder.DropIndex(
                name: "IX_TEstructuras_PuestoId",
                table: "TEstructuras");

            migrationBuilder.DropIndex(
                name: "IX_TEstructuras_SitioId",
                table: "TEstructuras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TDepartamentos",
                table: "TDepartamentos");

            migrationBuilder.DropColumn(
                name: "EstructuraId",
                table: "TEmpleados");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "TEstructuras");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "TEstructuras");

            migrationBuilder.DropColumn(
                name: "PuestoId",
                table: "TEstructuras");

            migrationBuilder.DropColumn(
                name: "SitioId",
                table: "TEstructuras");

            migrationBuilder.RenameTable(
                name: "TEstructuras",
                newName: "Estructura");

            migrationBuilder.RenameTable(
                name: "TDepartamentos",
                newName: "Departamentos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estructura",
                table: "Estructura",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamentos",
                table: "Departamentos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmpleadoEstructura",
                columns: table => new
                {
                    EstructuraId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoEstructura", x => new { x.EstructuraId, x.EmpleadoId });
                    table.ForeignKey(
                        name: "FK_EmpleadoEstructura_TEmpleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "TEmpleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoEstructura_Estructura_EstructuraId",
                        column: x => x.EstructuraId,
                        principalTable: "Estructura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoEstructura_EmpleadoId",
                table: "EmpleadoEstructura",
                column: "EmpleadoId");
        }
    }
}
