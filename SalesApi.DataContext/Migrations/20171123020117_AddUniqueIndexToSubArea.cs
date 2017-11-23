using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddUniqueIndexToSubArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubAreas_DeliveryVehicleId",
                table: "SubAreas");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_DeliveryVehicleId_Name",
                table: "SubAreas",
                columns: new[] { "DeliveryVehicleId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubAreas_DeliveryVehicleId_Name",
                table: "SubAreas");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_DeliveryVehicleId",
                table: "SubAreas",
                column: "DeliveryVehicleId");
        }
    }
}
