using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Time_Chat_Application.Migrations
{
    public partial class UpdatedtablesandcreatedChatRoomUsertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_UserId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChatRooms_ChatRooms_ChatRoomsId",
                table: "UserChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChatRooms_Users_UsersId",
                table: "UserChatRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRooms",
                table: "ChatRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChatRooms",
                table: "UserChatRooms");

            migrationBuilder.RenameTable(
                name: "UserChatRooms",
                newName: "ChatRoomUser");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ChatRooms",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "ChatMessages",
                newName: "ChatRoomID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ChatMessages",
                newName: "SenderID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ChatMessages",
                newName: "ReceiverID");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_ChatRoomId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_ChatRoomID");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_SenderID");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ChatRoomUser",
                newName: "UsersUserId");

            migrationBuilder.RenameColumn(
                name: "ChatRoomsId",
                table: "ChatRoomUser",
                newName: "ChatRoomsRoomID");

            migrationBuilder.RenameIndex(
                name: "IX_UserChatRooms_UsersId",
                table: "ChatRoomUser",
                newName: "IX_ChatRoomUser_UsersUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverID",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRooms",
                table: "ChatRooms",
                column: "RoomID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRoomUser",
                table: "ChatRoomUser",
                columns: new[] { "ChatRoomsRoomID", "UsersUserId" });

            migrationBuilder.CreateTable(
                name: "ChatRoomUsers",
                columns: table => new
                {
                    ChatRoomID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChatRoomUserID = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomUsers", x => new { x.ChatRoomID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ChatRoomUsers_ChatRooms_ChatRoomID",
                        column: x => x.ChatRoomID,
                        principalTable: "ChatRooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoomUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_CreatedBy",
                table: "ChatRooms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverID",
                table: "ChatMessages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomUsers_UserID",
                table: "ChatRoomUsers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomID",
                table: "ChatMessages",
                column: "ChatRoomID",
                principalTable: "ChatRooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_ReceiverID",
                table: "ChatMessages",
                column: "ReceiverID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_SenderID",
                table: "ChatMessages",
                column: "SenderID",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_CreatedBy",
                table: "ChatRooms",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomUser_ChatRooms_ChatRoomsRoomID",
                table: "ChatRoomUser",
                column: "ChatRoomsRoomID",
                principalTable: "ChatRooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomUser_Users_UsersUserId",
                table: "ChatRoomUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomID",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_ReceiverID",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_SenderID",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_CreatedBy",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomUser_ChatRooms_ChatRoomsRoomID",
                table: "ChatRoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomUser_Users_UsersUserId",
                table: "ChatRoomUser");

            migrationBuilder.DropTable(
                name: "ChatRoomUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRooms",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_CreatedBy",
                table: "ChatRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ReceiverID",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRoomUser",
                table: "ChatRoomUser");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "ChatRoomUser",
                newName: "UserChatRooms");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ChatRooms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ChatRoomID",
                table: "ChatMessages",
                newName: "ChatRoomId");

            migrationBuilder.RenameColumn(
                name: "SenderID",
                table: "ChatMessages",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverID",
                table: "ChatMessages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_ChatRoomID",
                table: "ChatMessages",
                newName: "IX_ChatMessages_ChatRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_SenderID",
                table: "ChatMessages",
                newName: "IX_ChatMessages_UserId");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "UserChatRooms",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ChatRoomsRoomID",
                table: "UserChatRooms",
                newName: "ChatRoomsId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRoomUser_UsersUserId",
                table: "UserChatRooms",
                newName: "IX_UserChatRooms_UsersId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRooms",
                table: "ChatRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChatRooms",
                table: "UserChatRooms",
                columns: new[] { "ChatRoomsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_UserId",
                table: "ChatMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatRooms_ChatRooms_ChatRoomsId",
                table: "UserChatRooms",
                column: "ChatRoomsId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatRooms_Users_UsersId",
                table: "UserChatRooms",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
