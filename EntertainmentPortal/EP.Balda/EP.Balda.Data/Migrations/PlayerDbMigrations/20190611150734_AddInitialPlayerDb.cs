using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.Balda.Data.Migrations.PlayerDbMigrations
{
    public partial class AddInitialPlayerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Players");

            migrationBuilder.CreateTable(
                "Players",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    NickName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Score = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Players", x => x.Id); });

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {1, "Daisha.Upton", "Tonya.Bauch", "BILs4", 85});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {3, "Troy71", "Taylor41", "7wlhc", 73});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {5, "Linda.Crona99", "Samantha_Tromp", "e7HW4", 99});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {7, "Alvera76", "Gilbert.Toy", "OvRL1", 44});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {9, "Elian.Greenholt", "Woodrow21", "mwY2N", 53});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {11, "Ruthe87", "Margie_Weissnat15", "SiRgM", 20});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {13, "Urban_Lind27", "Morris_Hettinger", "2VwoR", 23});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {15, "Dayna.MacGyver", "Erin.Goyette", "avwDB", 13});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {17, "Rowena_Walker", "Sonya.Block50", "SvYvV", 78});

            migrationBuilder.InsertData(
                "Players",
                new[] {"Id", "Login", "NickName", "Password", "Score"},
                new object[] {19, "Stewart_Macejkovic60", "Viola73", "Qy9Xy", 59});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Players");
        }
    }
}