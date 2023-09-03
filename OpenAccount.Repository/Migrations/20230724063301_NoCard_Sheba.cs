using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class NoCard_Sheba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardPrice",
                table: "AccountTypeSetting");

            migrationBuilder.AddColumn<string>(
                name: "ShebaNumber",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "شماره شبا");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 24, 10, 3, 1, 160, DateTimeKind.Local).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 24, 10, 3, 1, 160, DateTimeKind.Local).AddTicks(7615));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShebaNumber",
                table: "UserAccount");

            migrationBuilder.AddColumn<long>(
                name: "CardPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "هزینه کارت");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CardPrice", "SysDate" },
                values: new object[] { 200000L, new DateTime(2023, 7, 23, 15, 43, 37, 570, DateTimeKind.Local).AddTicks(6677) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CardPrice", "SysDate" },
                values: new object[] { 200000L, new DateTime(2023, 7, 23, 15, 43, 37, 570, DateTimeKind.Local).AddTicks(6724) });
        }
    }
}
