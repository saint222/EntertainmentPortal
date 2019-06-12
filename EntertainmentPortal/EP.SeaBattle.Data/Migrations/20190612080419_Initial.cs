using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.SeaBattle.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    CanShoot = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Player1Id = table.Column<string>(nullable: true),
                    Player2Id = table.Column<string>(nullable: true),
                    Finish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    PlayerId = table.Column<string>(nullable: true),
                    GameId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ships_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cells",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    X = table.Column<byte>(nullable: false),
                    Y = table.Column<byte>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ShipDbId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cells_Ships_ShipDbId",
                        column: x => x.ShipDbId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_ShipDbId",
                table: "Cells",
                column: "ShipDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player1Id",
                table: "Games",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player2Id",
                table: "Games",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_GameId",
                table: "Ships",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_PlayerId",
                table: "Ships",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cells");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
