using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tabaarak.Migrations
{
    /// <inheritdoc />
    public partial class TableCustomerDateTickettypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketName",
                table: "TicketBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketName",
                table: "TicketBookings");
        }
    }
}
