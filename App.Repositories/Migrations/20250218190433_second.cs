using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipant_AspNetUsers_UserId",
                table: "EventParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipant_Events_EventId",
                table: "EventParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipant",
                table: "EventParticipant");

            migrationBuilder.RenameTable(
                name: "EventParticipant",
                newName: "EventParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipant_UserId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipant_EventId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_AspNetUsers_UserId",
                table: "EventParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_AspNetUsers_UserId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Events_EventId",
                table: "EventParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventParticipants",
                table: "EventParticipants");

            migrationBuilder.RenameTable(
                name: "EventParticipants",
                newName: "EventParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_UserId",
                table: "EventParticipant",
                newName: "IX_EventParticipant_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_EventId",
                table: "EventParticipant",
                newName: "IX_EventParticipant_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventParticipant",
                table: "EventParticipant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipant_AspNetUsers_UserId",
                table: "EventParticipant",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipant_Events_EventId",
                table: "EventParticipant",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
