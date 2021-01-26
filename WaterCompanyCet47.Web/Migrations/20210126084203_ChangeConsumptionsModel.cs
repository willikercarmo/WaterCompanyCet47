using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompanyCet47.Web.Migrations
{
    public partial class ChangeConsumptionsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForYear",
                table: "ConsumptionDetailTemps");

            migrationBuilder.DropColumn(
                name: "ForYear",
                table: "ConsumptionDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ForMonth",
                table: "ConsumptionDetailTemps",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ForMonth",
                table: "ConsumptionDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ForMonth",
                table: "ConsumptionDetailTemps",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForYear",
                table: "ConsumptionDetailTemps",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ForMonth",
                table: "ConsumptionDetails",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForYear",
                table: "ConsumptionDetails",
                nullable: true);
        }
    }
}
