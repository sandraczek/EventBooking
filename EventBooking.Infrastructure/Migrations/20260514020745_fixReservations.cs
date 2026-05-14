using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Events_EventId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_EventId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId1",
                table: "Reservations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventId1",
                table: "Reservations",
                column: "EventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Events_EventId1",
                table: "Reservations",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
