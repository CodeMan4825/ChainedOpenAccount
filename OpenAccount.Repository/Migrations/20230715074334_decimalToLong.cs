using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class decimalToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Used",
                table: "WithdrawalFromWallet",
                type: "bit",
                nullable: false,
                comment: "مبلغ کسر شده استفاده شد؟",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "مبلغ از کیف پول کسر شد");

            migrationBuilder.AlterColumn<long>(
                name: "OpenAccountPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "هزینه افتتاح حساب",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "هزینه افتتاح حساب");

            migrationBuilder.AlterColumn<long>(
                name: "MinBalance",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "کف حساب",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "کف حساب");

            migrationBuilder.AlterColumn<long>(
                name: "InqueryPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "هزینه استعلام",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "هزینه استعلام");

            migrationBuilder.AlterColumn<long>(
                name: "CardPrice",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                comment: "هزینه کارت",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "هزینه کارت");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CardPrice", "InqueryPrice", "MinBalance", "OpenAccountPrice", "SysDate" },
                values: new object[] { 200000L, 20000L, 1000000L, 40000L, new DateTime(2023, 7, 15, 11, 13, 34, 565, DateTimeKind.Local).AddTicks(6616) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CardPrice", "InqueryPrice", "MinBalance", "OpenAccountPrice", "SysDate" },
                values: new object[] { 200000L, 20000L, 330000L, 40000L, new DateTime(2023, 7, 15, 11, 13, 34, 565, DateTimeKind.Local).AddTicks(6651) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Used",
                table: "WithdrawalFromWallet",
                type: "bit",
                nullable: false,
                comment: "مبلغ از کیف پول کسر شد",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "مبلغ کسر شده استفاده شد؟");

            migrationBuilder.AlterColumn<decimal>(
                name: "OpenAccountPrice",
                table: "AccountTypeSetting",
                type: "decimal(18,2)",
                nullable: false,
                comment: "هزینه افتتاح حساب",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "هزینه افتتاح حساب");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinBalance",
                table: "AccountTypeSetting",
                type: "decimal(18,2)",
                nullable: false,
                comment: "کف حساب",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "کف حساب");

            migrationBuilder.AlterColumn<decimal>(
                name: "InqueryPrice",
                table: "AccountTypeSetting",
                type: "decimal(18,2)",
                nullable: false,
                comment: "هزینه استعلام",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "هزینه استعلام");

            migrationBuilder.AlterColumn<decimal>(
                name: "CardPrice",
                table: "AccountTypeSetting",
                type: "decimal(18,2)",
                nullable: false,
                comment: "هزینه کارت",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "هزینه کارت");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CardPrice", "InqueryPrice", "MinBalance", "OpenAccountPrice", "SysDate" },
                values: new object[] { 200000m, 20000m, 1000000m, 40000m, new DateTime(2023, 7, 12, 7, 52, 49, 444, DateTimeKind.Local).AddTicks(9696) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CardPrice", "InqueryPrice", "MinBalance", "OpenAccountPrice", "SysDate" },
                values: new object[] { 200000m, 20000m, 330000m, 40000m, new DateTime(2023, 7, 12, 7, 52, 49, 444, DateTimeKind.Local).AddTicks(9735) });
        }
    }
}
