using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MQTTLAB.Share.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "topic",
                table: "t_sensor_data");

            migrationBuilder.AddColumn<string>(
                name: "topic",
                table: "t_sensor",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "topic",
                table: "t_sensor");

            migrationBuilder.AddColumn<string>(
                name: "topic",
                table: "t_sensor_data",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
