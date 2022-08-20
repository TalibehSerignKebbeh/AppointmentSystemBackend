using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appointmentApi.Migrations
{
    public partial class addingIsCancelField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCancell",
                table: "appoinments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCancell",
                table: "appoinments");
        }
    }
}
