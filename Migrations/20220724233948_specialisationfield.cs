using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appointmentApi.Migrations
{
    public partial class specialisationfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "specialisation",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specialisation",
                table: "users");
        }
    }
}
