using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class EstructurasEmpleadoupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "TEstructuras",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras",
                column: "EmpleadoId",
                principalTable: "TEmpleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "TEstructuras",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras",
                column: "EmpleadoId",
                principalTable: "TEmpleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
