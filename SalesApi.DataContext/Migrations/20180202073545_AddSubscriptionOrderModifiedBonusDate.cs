using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddSubscriptionOrderModifiedBonusDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionOrderModifiedBonusDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DayCount = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastAction = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    SubscriptionOrderId = table.Column<int>(nullable: false),
                    SubscriptionProductSnapshotId = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOrderModifiedBonusDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderModifiedBonusDates_SubscriptionOrders_SubscriptionOrderId",
                        column: x => x.SubscriptionOrderId,
                        principalTable: "SubscriptionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionOrderModifiedBonusDates_SubscriptionProductSnapshots_SubscriptionProductSnapshotId",
                        column: x => x.SubscriptionProductSnapshotId,
                        principalTable: "SubscriptionProductSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderModifiedBonusDates_SubscriptionProductSnapshotId",
                table: "SubscriptionOrderModifiedBonusDates",
                column: "SubscriptionProductSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOrderModifiedBonusDates_SubscriptionOrderId_SubscriptionProductSnapshotId_Date",
                table: "SubscriptionOrderModifiedBonusDates",
                columns: new[] { "SubscriptionOrderId", "SubscriptionProductSnapshotId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionOrderModifiedBonusDates");
        }
    }
}
