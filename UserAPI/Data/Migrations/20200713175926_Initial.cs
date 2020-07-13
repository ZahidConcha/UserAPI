using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TPuestos_Departamentos_DepartamentosId",
                table: "TPuestos");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_TPuestos_DepartamentosId",
                table: "TPuestos");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "TPuestos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TDepartamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    SitioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDepartamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TDepartamentos_TSitios_SitioId",
                        column: x => x.SitioId,
                        principalTable: "TSitios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEstructura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEstructura", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TPuestos_DepartamentoId",
                table: "TPuestos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TDepartamentos_SitioId",
                table: "TDepartamentos",
                column: "SitioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TPuestos_TDepartamentos_DepartamentoId",
                table: "TPuestos",
                column: "DepartamentoId",
                principalTable: "TDepartamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TPuestos_TDepartamentos_DepartamentoId",
                table: "TPuestos");

            migrationBuilder.DropTable(
                name: "TDepartamentos");

            migrationBuilder.DropTable(
                name: "TEstructura");

            migrationBuilder.DropIndex(
                name: "IX_TPuestos_DepartamentoId",
                table: "TPuestos");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "TPuestos");

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SitioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamentos_TSitios_SitioId",
                        column: x => x.SitioId,
                        principalTable: "TSitios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TPuestos_DepartamentosId",
                table: "TPuestos",
                column: "DepartamentosId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_SitioId",
                table: "Departamentos",
                column: "SitioId");

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
