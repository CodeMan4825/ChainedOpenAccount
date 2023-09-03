using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class Setting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenAccountPrice",
                table: "AccountTypeSetting");

            migrationBuilder.AlterColumn<long>(
                name: "MinBalance",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "موجودی اولیه حساب",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "کف حساب");

            migrationBuilder.AlterColumn<long>(
                name: "InqueryPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "استعلام چک و تسهیلات معوق - سمات و سماچک",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "هزینه استعلام");

            migrationBuilder.AddColumn<long>(
                name: "IdentificationInquiry",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "استعلام ثبت احوال");

            migrationBuilder.AddColumn<long>(
                name: "PostalCodeInquiry",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "استعلام کد پستی");

            migrationBuilder.AddColumn<int>(
                name: "Stamp",
                table: "AccountTypeSetting",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "تمبر");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "IdentificationInquiry", "InqueryPrice", "PostalCodeInquiry", "Stamp", "SysDate" },
                values: new object[] { 4500L, 0L, 15000L, 10000, new DateTime(2023, 7, 24, 14, 52, 12, 414, DateTimeKind.Local).AddTicks(543) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "IdentificationInquiry", "InqueryPrice", "MinBalance", "PostalCodeInquiry", "Stamp", "SysDate" },
                values: new object[] { 4500L, 0L, 500000L, 15000L, 10000, new DateTime(2023, 7, 24, 14, 52, 12, 414, DateTimeKind.Local).AddTicks(579) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationInquiry",
                table: "AccountTypeSetting");

            migrationBuilder.DropColumn(
                name: "PostalCodeInquiry",
                table: "AccountTypeSetting");

            migrationBuilder.DropColumn(
                name: "Stamp",
                table: "AccountTypeSetting");

            migrationBuilder.AlterColumn<long>(
                name: "MinBalance",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "کف حساب",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "موجودی اولیه حساب");

            migrationBuilder.AlterColumn<long>(
                name: "InqueryPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "هزینه استعلام",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "استعلام چک و تسهیلات معوق - سمات و سماچک");

            migrationBuilder.AddColumn<long>(
                name: "OpenAccountPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "هزینه افتتاح حساب");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "InqueryPrice", "OpenAccountPrice", "SysDate" },
                values: new object[] { 20000L, 40000L, new DateTime(2023, 7, 24, 10, 3, 1, 160, DateTimeKind.Local).AddTicks(7533) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "InqueryPrice", "MinBalance", "OpenAccountPrice", "SysDate" },
                values: new object[] { 20000L, 330000L, 40000L, new DateTime(2023, 7, 24, 10, 3, 1, 160, DateTimeKind.Local).AddTicks(7615) });
        }
    }
}
