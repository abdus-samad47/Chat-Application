using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Time_Chat_Application.Migrations
{
    public partial class AddRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomUser_ChatRoomUser_RoomId",
                table: "ChatRoomUser",
                column: "ChatRoomUser_RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomUser_ChatRoomUser_UserId",
                table: "ChatRoomUser",
                column: "ChatRoomUser_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_Room_CreatedBy",
                table: "ChatRooms",
                column: "Room_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatMessage_Receiver",
                table: "ChatMessages",
                column: "ChatMessage_Receiver");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatMessage_RoomId",
                table: "ChatMessages",
                column: "ChatMessage_RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatMessage_Sender",
                table: "ChatMessages",
                column: "ChatMessage_Sender");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatMessage_RoomId",
                table: "ChatMessages",
                column: "ChatMessage_RoomId",
                principalTable: "ChatRooms",
                principalColumn: "Room_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_ChatMessage_Receiver",
                table: "ChatMessages",
                column: "ChatMessage_Receiver",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_ChatMessage_Sender",
                table: "ChatMessages",
                column: "ChatMessage_Sender",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_Room_CreatedBy",
                table: "ChatRooms",
                column: "Room_CreatedBy",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomUser_ChatRooms_ChatRoomUser_RoomId",
                table: "ChatRoomUser",
                column: "ChatRoomUser_RoomId",
                principalTable: "ChatRooms",
                principalColumn: "Room_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomUser_Users_ChatRoomUser_UserId",
                table: "ChatRoomUser",
                column: "ChatRoomUser_UserId",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatMessage_RoomId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_ChatMessage_Receiver",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_ChatMessage_Sender",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_Room_CreatedBy",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomUser_ChatRooms_ChatRoomUser_RoomId",
                table: "ChatRoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomUser_Users_ChatRoomUser_UserId",
                table: "ChatRoomUser");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoomUser_ChatRoomUser_RoomId",
                table: "ChatRoomUser");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoomUser_ChatRoomUser_UserId",
                table: "ChatRoomUser");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_Room_CreatedBy",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ChatMessage_Receiver",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ChatMessage_RoomId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ChatMessage_Sender",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatRooms");
        }
    }
}
