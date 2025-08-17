using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBot.Migrations
{
    /// <inheritdoc />
    public partial class reminder_is_set : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Reminders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_EventId",
                table: "Reminders",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Events_EventId",
                table: "Reminders",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Events_EventId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_EventId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Reminders");
        }
    }
}
