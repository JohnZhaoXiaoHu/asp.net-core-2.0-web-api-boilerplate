using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SalesApi.DataContext.Migrations
{
    public partial class AddRetailer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Retailer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    LastAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegacyCustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pinyin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesType = table.Column<int>(type: "int", nullable: false),
                    SubAreaId = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retailer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retailer_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Retailer_SubAreaId",
                table: "Retailer",
                column: "SubAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Retailer");
        }
    }
}
