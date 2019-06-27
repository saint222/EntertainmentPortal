using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.TicTacToe.Data.Migrations.GameDbMigrations
{
    public partial class InitialCreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Games",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table => { table.PrimaryKey("PK_Games", x => x.Id); });

            migrationBuilder.CreateTable(
                "Maps",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Size = table.Column<int>(),
                    GameId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                    table.ForeignKey(
                        "FK_Maps_Games_GameId",
                        x => x.GameId,
                        "Games",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Steps",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        "FK_Steps_Games_GameId",
                        x => x.GameId,
                        "Games",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Cells",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    MapId = table.Column<int>(),
                    X = table.Column<int>(),
                    Y = table.Column<int>(),
                    TicTac = table.Column<char>(nullable: true),
                    StepId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.Id);
                    table.ForeignKey(
                        "FK_Cells_Maps_MapId",
                        x => x.MapId,
                        "Maps",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Cells_Steps_StepId",
                        x => x.StepId,
                        "Steps",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Players",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    NickName = table.Column<string>(maxLength: 30),
                    Login = table.Column<string>(maxLength: 30),
                    Password = table.Column<string>(maxLength: 8),
                    StepId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        "FK_Players_Steps_StepId",
                        x => x.StepId,
                        "Steps",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PlayerGames",
                table => new
                {
                    PlayerId = table.Column<int>(),
                    GameId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGames", x => new {x.PlayerId, x.GameId});
                    table.ForeignKey(
                        "FK_PlayerGames_Games_GameId",
                        x => x.GameId,
                        "Games",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PlayerGames_Players_PlayerId",
                        x => x.PlayerId,
                        "Players",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Cells_MapId",
                "Cells",
                "MapId");

            migrationBuilder.CreateIndex(
                "IX_Cells_StepId",
                "Cells",
                "StepId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Maps_GameId",
                "Maps",
                "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_PlayerGames_GameId",
                "PlayerGames",
                "GameId");

            migrationBuilder.CreateIndex(
                "IX_Players_StepId",
                "Players",
                "StepId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Steps_GameId",
                "Steps",
                "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Cells");

            migrationBuilder.DropTable(
                "PlayerGames");

            migrationBuilder.DropTable(
                "Maps");

            migrationBuilder.DropTable(
                "Players");

            migrationBuilder.DropTable(
                "Steps");

            migrationBuilder.DropTable(
                "Games");
        }
    }
}