using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ColumnSizeBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "BoxesTypes",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Lenght",
                table: "BoxesTypes",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LengthUnit",
                table: "BoxesTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxProductsItems",
                table: "BoxesTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "BoxesTypes",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PorcentFull",
                table: "Boxes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "BoxesTypes");

            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "BoxesTypes");

            migrationBuilder.DropColumn(
                name: "LengthUnit",
                table: "BoxesTypes");

            migrationBuilder.DropColumn(
                name: "MaxProductsItems",
                table: "BoxesTypes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "BoxesTypes");

            migrationBuilder.DropColumn(
                name: "PorcentFull",
                table: "Boxes");
        }
    }
}
