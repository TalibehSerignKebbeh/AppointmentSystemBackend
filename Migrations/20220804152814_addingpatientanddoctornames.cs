using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appointmentApi.Migrations
{
    public partial class addingpatientanddoctornames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "time",
                table: "appoinments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "doctorName",
                table: "appoinments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "patientName",
                table: "appoinments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctorName",
                table: "appoinments");

            migrationBuilder.DropColumn(
                name: "patientName",
                table: "appoinments");

            migrationBuilder.AlterColumn<string>(
                name: "time",
                table: "appoinments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
