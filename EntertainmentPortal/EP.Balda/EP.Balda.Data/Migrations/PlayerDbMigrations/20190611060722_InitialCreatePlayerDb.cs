using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.Balda.Data.Migrations.PlayerDbMigrations
{
    public partial class InitialCreatePlayerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Players",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Sqlite:Autoincrement", true),
                    NickName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Score = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Players", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Players");
        }
    }
}