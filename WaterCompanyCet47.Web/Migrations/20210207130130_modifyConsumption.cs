using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterCompanyCet47.Web.Migrations
{
    public partial class modifyConsumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsumptionDate",
                table: "Consumptions",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsumptionDate",
                table: "Consumptions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
