using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Time_Chat_Application.Migrations
{
    public partial class applyConstraintsOnUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_User_Email",
                table: "Users",
                column: "User_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_name",
                table: "Users",
                column: "User_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_User_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_User_name",
                table: "Users");
        }
    }
}
