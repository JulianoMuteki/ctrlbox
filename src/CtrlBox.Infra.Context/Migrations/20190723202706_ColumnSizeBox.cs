using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ColumnSizeBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Boxes",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Lenght",
                table: "Boxes",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LengthUnit",
                table: "Boxes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxProductsItems",
                table: "Boxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PorcentFull",
                table: "Boxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Boxes",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "LengthUnit",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "MaxProductsItems",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "PorcentFull",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Boxes");
        }
    }
}
