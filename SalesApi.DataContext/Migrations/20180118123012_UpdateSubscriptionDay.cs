using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class UpdateSubscriptionDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Initialized",
                table: "SubscriptionDays");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SubscriptionDays",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubscriptionDays");

            migrationBuilder.AddColumn<bool>(
                name: "Initialized",
                table: "SubscriptionDays",
                nullable: false,
                defaultValue: false);
        }
    }
}
