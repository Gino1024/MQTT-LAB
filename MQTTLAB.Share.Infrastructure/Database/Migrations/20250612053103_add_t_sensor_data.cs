using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MQTTLAB.Share.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class add_t_sensor_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "t_sensor",
                newName: "created_at");

            migrationBuilder.CreateTable(
                name: "t_sensor_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sensor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_sensor_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_sensor_data_t_sensor_sensor_id",
                        column: x => x.sensor_id,
                        principalTable: "t_sensor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_sensor_data_sensor_id",
                table: "t_sensor_data",
                column: "sensor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_sensor_data");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "t_sensor",
                newName: "createdAt");
        }
    }
}
