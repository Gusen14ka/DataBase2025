using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Services_ServiceId",
                table: "Timetables");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarId",
                table: "Orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedServices_OrderId",
                table: "OrderedServices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedServices_ServiceId",
                table: "OrderedServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedParts_OrderId",
                table: "OrderedParts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedParts_PartId",
                table: "OrderedParts",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedParts_Orders_OrderId",
                table: "OrderedParts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedParts_Parts_PartId",
                table: "OrderedParts",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedServices_Orders_OrderId",
                table: "OrderedServices",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedServices_Services_ServiceId",
                table: "OrderedServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_CarId",
                table: "Orders",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Services_ServiceId",
                table: "Timetables",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedParts_Orders_OrderId",
                table: "OrderedParts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedParts_Parts_PartId",
                table: "OrderedParts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedServices_Orders_OrderId",
                table: "OrderedServices");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedServices_Services_ServiceId",
                table: "OrderedServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_CarId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Services_ServiceId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CarId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderedServices_OrderId",
                table: "OrderedServices");

            migrationBuilder.DropIndex(
                name: "IX_OrderedServices_ServiceId",
                table: "OrderedServices");

            migrationBuilder.DropIndex(
                name: "IX_OrderedParts_OrderId",
                table: "OrderedParts");

            migrationBuilder.DropIndex(
                name: "IX_OrderedParts_PartId",
                table: "OrderedParts");

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Services_ServiceId",
                table: "Timetables",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
