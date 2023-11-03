using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class Addbilltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OperationName = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillModelServiceModel",
                columns: table => new
                {
                    BillsId = table.Column<string>(type: "TEXT", nullable: false),
                    ServicesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillModelServiceModel", x => new { x.BillsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BillModelServiceModel_Bills_BillsId",
                        column: x => x.BillsId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillModelServiceModel_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillModelServiceModel_ServicesId",
                table: "BillModelServiceModel",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ClientId",
                table: "Bills",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillModelServiceModel");

            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
