using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarBot.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    TimeBefore = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    ChatId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Token = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    RefreshTokenDtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccessTokenDtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccessTokenExpirationUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.ChatId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
