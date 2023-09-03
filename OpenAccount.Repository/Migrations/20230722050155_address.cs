using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                comment: "MobileNumber");

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                comment: "تلفن ثابت");

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                comment: "تلفن ثابت");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 31, 54, 663, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 31, 54, 663, DateTimeKind.Local).AddTicks(5267));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Address");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 22, 35, 957, DateTimeKind.Local).AddTicks(7834));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 22, 35, 957, DateTimeKind.Local).AddTicks(7875));
        }
    }
}
