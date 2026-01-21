using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPath.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Accounts_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                schema: "accounts",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_tokens",
                schema: "accounts",
                table: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "accounts",
                newName: "user_tokens",
                newSchema: "accounts");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_tokens",
                schema: "accounts",
                table: "user_tokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddForeignKey(
                name: "fk_user_tokens_asp_net_users_user_id",
                schema: "accounts",
                table: "user_tokens",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_tokens_asp_net_users_user_id",
                schema: "accounts",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_tokens",
                schema: "accounts",
                table: "user_tokens");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                schema: "accounts",
                newName: "AspNetUserTokens",
                newSchema: "accounts");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_tokens",
                schema: "accounts",
                table: "AspNetUserTokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                schema: "accounts",
                table: "AspNetUserTokens",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
