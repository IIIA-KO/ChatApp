using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relations_Simplification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_participants_chats_chats_id",
                table: "chat_participants");

            migrationBuilder.DropForeignKey(
                name: "fk_chat_participants_users_participants_id",
                table: "chat_participants");

            migrationBuilder.DropForeignKey(
                name: "fk_chats_users_creator_id",
                table: "chats");

            migrationBuilder.RenameColumn(
                name: "participants_id",
                table: "chat_participants",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "chats_id",
                table: "chat_participants",
                newName: "chat_id");

            migrationBuilder.RenameIndex(
                name: "ix_chat_participants_participants_id",
                table: "chat_participants",
                newName: "ix_chat_participants_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_chat_participants_chats_chat_id",
                table: "chat_participants",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chat_participants_users_user_id",
                table: "chat_participants",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chats_users_creator_id",
                table: "chats",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_participants_chats_chat_id",
                table: "chat_participants");

            migrationBuilder.DropForeignKey(
                name: "fk_chat_participants_users_user_id",
                table: "chat_participants");

            migrationBuilder.DropForeignKey(
                name: "fk_chats_users_creator_id",
                table: "chats");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "chat_participants",
                newName: "participants_id");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                table: "chat_participants",
                newName: "chats_id");

            migrationBuilder.RenameIndex(
                name: "ix_chat_participants_user_id",
                table: "chat_participants",
                newName: "ix_chat_participants_participants_id");

            migrationBuilder.AddForeignKey(
                name: "fk_chat_participants_chats_chats_id",
                table: "chat_participants",
                column: "chats_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chat_participants_users_participants_id",
                table: "chat_participants",
                column: "participants_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chats_users_creator_id",
                table: "chats",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
