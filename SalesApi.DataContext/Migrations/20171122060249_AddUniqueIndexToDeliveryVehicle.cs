using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddUniqueIndexToDeliveryVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId_DistributionGroupId",
                table: "DeliveryVehicles",
                columns: new[] { "SalesType", "VehicleId", "DistributionGroupId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryVehicles_SalesType_VehicleId_DistributionGroupId",
                table: "DeliveryVehicles");
        }
    }
}
