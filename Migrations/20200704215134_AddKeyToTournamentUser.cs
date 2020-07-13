using Microsoft.EntityFrameworkCore.Migrations;

namespace Pai.Migrations
{
    public partial class AddKeyToTournamentUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TournamentUser",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUser_UserId",
                table: "TournamentUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentUser_AspNetUsers_UserId",
                table: "TournamentUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentUser_AspNetUsers_UserId",
                table: "TournamentUser");

            migrationBuilder.DropIndex(
                name: "IX_TournamentUser_UserId",
                table: "TournamentUser");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TournamentUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
