using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabaarak.Migrations
{
    /// <inheritdoc />
    public partial class TableCustomerDateColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "TicketBookings");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "TicketBookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                table: "TicketBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "TicketBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
