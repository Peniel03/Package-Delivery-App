using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.DataAccess.Migrations
{
    public partial class AddedUserIdToLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Locations");
        }
    }
}
