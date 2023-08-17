using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace codefirst_deneme.Migrations
{
    /// <inheritdoc />
    public partial class rol_yapisi_duzenlendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicis_Rols_RolId",
                table: "Kullanicis");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicis_RolId",
                table: "Kullanicis");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Kullanicis");

            migrationBuilder.AddColumn<string>(
                name: "Eposta",
                table: "Kullanicis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "KullaniciRols",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciRols", x => new { x.RolId, x.KullaniciId });
                    table.ForeignKey(
                        name: "FK_KullaniciRols_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciRols_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciRols_KullaniciId",
                table: "KullaniciRols",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciRols");

            migrationBuilder.DropColumn(
                name: "Eposta",
                table: "Kullanicis");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Kullanicis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicis_RolId",
                table: "Kullanicis",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicis_Rols_RolId",
                table: "Kullanicis",
                column: "RolId",
                principalTable: "Rols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
