using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventImage",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "CommentImage",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentImage",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "EventImage",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
