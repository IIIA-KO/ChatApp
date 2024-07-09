using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Use_of_JoinTable_as_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "chat_user");

            migrationBuilder.CreateTable(
                name: "users_chats",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_chats", x => new { x.user_id, x.chat_id });
                    table.ForeignKey(
                        name: "fk_users_chats_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_users_chats_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_users_chats_chat_id",
                table: "users_chats",
                column: "chat_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users_chats");

            migrationBuilder.CreateTable(
                name: "chat_user",
                columns: table => new
                {
                    chats_id = table.Column<Guid>(type: "uuid", nullable: false),
                    participants_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_user", x => new { x.chats_id, x.participants_id });
                    table.ForeignKey(
                        name: "fk_chat_user_chats_chats_id",
                        column: x => x.chats_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_chat_user_users_participants_id",
                        column: x => x.participants_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_chat_user_participants_id",
                table: "chat_user",
                column: "participants_id"
            );
        }
    }
}
