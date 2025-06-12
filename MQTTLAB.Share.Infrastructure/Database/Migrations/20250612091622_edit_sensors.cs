using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MQTTLAB.Share.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class edit_sensors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "t_sensor_data");

            migrationBuilder.AlterColumn<double>(
                name: "value",
                table: "t_sensor_data",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "topic",
                table: "t_sensor_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "t_sensor_data",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "topic",
                table: "t_sensor_data");

            migrationBuilder.DropColumn(
                name: "unit",
                table: "t_sensor_data");

            migrationBuilder.AlterColumn<int>(
                name: "value",
                table: "t_sensor_data",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "t_sensor_data",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
