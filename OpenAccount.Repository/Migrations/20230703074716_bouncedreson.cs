using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class bouncedreson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Int",
                table: "SamatChequeBouncedReason",
                type: "int",
                nullable: false,
                comment: "Reason of bounce as int",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IntDescription",
                table: "SamatChequeBouncedReason",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Reason of bounce description");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 3, 11, 17, 16, 99, DateTimeKind.Local).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 3, 11, 17, 16, 99, DateTimeKind.Local).AddTicks(8314));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntDescription",
                table: "SamatChequeBouncedReason");

            migrationBuilder.AlterColumn<int>(
                name: "Int",
                table: "SamatChequeBouncedReason",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Reason of bounce as int");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 2, 11, 41, 0, 58, DateTimeKind.Local).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 2, 11, 41, 0, 58, DateTimeKind.Local).AddTicks(989));
        }
    }
}
