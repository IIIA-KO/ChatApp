using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relations_Simplification2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "chat_participants");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "chat_user");

            migrationBuilder.CreateTable(
                name: "chat_participants",
                columns: table => new
                {
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_participants", x => new { x.chat_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_chat_participants_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_chat_participants_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_chat_participants_user_id",
                table: "chat_participants",
                column: "user_id"
            );
        }
    }
}
