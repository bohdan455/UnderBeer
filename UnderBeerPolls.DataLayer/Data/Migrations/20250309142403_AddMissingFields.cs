using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnderBeerPolls.DataLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Polls",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PollOptionId",
                table: "PollOptionResponses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PollOptionResponses_PollOptionId",
                table: "PollOptionResponses",
                column: "PollOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptionResponses_PollOptions_PollOptionId",
                table: "PollOptionResponses",
                column: "PollOptionId",
                principalTable: "PollOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOptionResponses_PollOptions_PollOptionId",
                table: "PollOptionResponses");

            migrationBuilder.DropIndex(
                name: "IX_PollOptionResponses_PollOptionId",
                table: "PollOptionResponses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "PollOptionId",
                table: "PollOptionResponses");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
