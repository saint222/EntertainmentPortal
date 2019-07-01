using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.Hangman.Data.Migrations
{
    public partial class ChangeStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectLettersAsString",
                table: "Games",
                newName: "CorrectLetters");

            migrationBuilder.RenameColumn(
                name: "AlphabetAsString",
                table: "Games",
                newName: "Alphabet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectLetters",
                table: "Games",
                newName: "CorrectLettersAsString");

            migrationBuilder.RenameColumn(
                name: "Alphabet",
                table: "Games",
                newName: "AlphabetAsString");
        }
    }
}
