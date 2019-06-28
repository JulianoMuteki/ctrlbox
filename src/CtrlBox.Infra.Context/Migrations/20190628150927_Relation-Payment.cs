using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class RelationPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentsMethods_PaymentMethodID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentSchedules_PaymentScheduleID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentScheduleID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentScheduleID",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentID",
                table: "PaymentSchedules",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodID",
                table: "PaymentSchedules",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedules_PaymentID",
                table: "PaymentSchedules",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedules_PaymentMethodID",
                table: "PaymentSchedules",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSchedules_Payments_PaymentID",
                table: "PaymentSchedules",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSchedules_PaymentsMethods_PaymentMethodID",
                table: "PaymentSchedules",
                column: "PaymentMethodID",
                principalTable: "PaymentsMethods",
                principalColumn: "PaymentMethodID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSchedules_Payments_PaymentID",
                table: "PaymentSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSchedules_PaymentsMethods_PaymentMethodID",
                table: "PaymentSchedules");

            migrationBuilder.DropIndex(
                name: "IX_PaymentSchedules_PaymentID",
                table: "PaymentSchedules");

            migrationBuilder.DropIndex(
                name: "IX_PaymentSchedules_PaymentMethodID",
                table: "PaymentSchedules");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "PaymentSchedules");

            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                table: "PaymentSchedules");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodID",
                table: "Payments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentScheduleID",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentScheduleID",
                table: "Payments",
                column: "PaymentScheduleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentsMethods_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID",
                principalTable: "PaymentsMethods",
                principalColumn: "PaymentMethodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentSchedules_PaymentScheduleID",
                table: "Payments",
                column: "PaymentScheduleID",
                principalTable: "PaymentSchedules",
                principalColumn: "PaymentScheduleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
