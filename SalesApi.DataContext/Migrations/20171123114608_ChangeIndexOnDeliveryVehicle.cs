using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class ChangeIndexOnDeliveryVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId_DistributionGroupId",
                table: "DeliveryVehicles");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId",
                table: "DeliveryVehicles",
                columns: new[] { "SalesType", "VehicleId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId",
                table: "DeliveryVehicles");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId_DistributionGroupId",
                table: "DeliveryVehicles",
                columns: new[] { "SalesType", "VehicleId", "DistributionGroupId" },
                unique: true);
        }
    }
}
