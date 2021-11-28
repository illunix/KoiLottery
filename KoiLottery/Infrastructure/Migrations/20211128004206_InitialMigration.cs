using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KoiLottery.infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lotteries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Repeat = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LotteryPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LotteryId = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotteryPayments_Lotteries_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lotteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotteryParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LotteryId = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotteryParticipants_Lotteries_LotteryId",
                        column: x => x.LotteryId,
                        principalTable: "Lotteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotteryParticipants_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotteryParticipants_LotteryId",
                table: "LotteryParticipants",
                column: "LotteryId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryParticipants_WalletId",
                table: "LotteryParticipants",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_LotteryPayments_LotteryId",
                table: "LotteryPayments",
                column: "LotteryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotteryParticipants");

            migrationBuilder.DropTable(
                name: "LotteryPayments");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Lotteries");
        }
    }
}
