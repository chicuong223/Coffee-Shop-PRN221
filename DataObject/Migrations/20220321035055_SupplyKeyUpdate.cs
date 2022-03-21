using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataObject.Migrations
{
    public partial class SupplyKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Supply__EA10D413C91BF00F",
                table: "Supply");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SupplyDate",
                table: "Supply",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Supply__EA10D413C91BF00F",
                table: "Supply",
                columns: new[] { "ProductID", "SupplierID", "SupplyDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Supply__EA10D413C91BF00F",
                table: "Supply");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SupplyDate",
                table: "Supply",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Supply__EA10D413C91BF00F",
                table: "Supply",
                columns: new[] { "ProductID", "SupplierID" });
        }
    }
}
