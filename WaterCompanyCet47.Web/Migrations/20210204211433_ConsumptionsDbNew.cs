using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompanyCet47.Web.Migrations
{
    public partial class ConsumptionsDbNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumptionValue",
                table: "Consumptions");

            migrationBuilder.DropColumn(
                name: "ForMonth",
                table: "Consumptions");

            migrationBuilder.AddColumn<int>(
                name: "ItemsId",
                table: "Consumptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_ItemsId",
                table: "Consumptions",
                column: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumptions_ConsumptionDetails_ItemsId",
                table: "Consumptions",
                column: "ItemsId",
                principalTable: "ConsumptionDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumptions_ConsumptionDetails_ItemsId",
                table: "Consumptions");

            migrationBuilder.DropIndex(
                name: "IX_Consumptions_ItemsId",
                table: "Consumptions");

            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "Consumptions");

            migrationBuilder.AddColumn<double>(
                name: "ConsumptionValue",
                table: "Consumptions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ForMonth",
                table: "Consumptions",
                nullable: true);
        }
    }
}
