using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoWatcher.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatingRelationWithCrytpoAndProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileHasCrypto",
                columns: table => new
                {
                    CryptoId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileHasCrypto", x => new { x.CryptoId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ProfileHasCrypto_cryptosInfo_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "cryptosInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileHasCrypto_profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileHasCrypto_ProfileId",
                table: "ProfileHasCrypto",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileHasCrypto");
        }
    }
}
