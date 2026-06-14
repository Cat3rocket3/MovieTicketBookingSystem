using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTickets.Migrations
{
    public partial class AddSeatNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Seats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Seats");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
