using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class EstructurasEmpleadoupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras");

            migrationBuilder.DropColumn(
                name: "EstructuraId",
                table: "TEmpleados");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "TEstructuras",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras",
                column: "EmpleadoId",
                principalTable: "TEmpleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EstructuraId",
                table: "TEmpleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TEstructuras_TEmpleados_EmpleadoId",
                table: "TEstructuras",
                column: "EmpleadoId",
                principalTable: "TEmpleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
