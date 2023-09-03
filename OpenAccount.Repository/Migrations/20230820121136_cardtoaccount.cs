using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
    /// <inheritdoc />
    public partial class cardtoaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.AddColumn<long>(
                name: "CardToAccount",
                table: "AccountTypeSetting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "کارمزد اتصال کارت به حساب");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                columns: new[] { "CardToAccount", "SysDate" },
                values: new object[] { 19800L, new DateTime(2023, 8, 20, 15, 41, 36, 433, DateTimeKind.Local).AddTicks(1326) });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "CardToAccount", "SysDate" },
                values: new object[] { 19800L, new DateTime(2023, 8, 20, 15, 41, 36, 433, DateTimeKind.Local).AddTicks(1366) });

            migrationBuilder.InsertData(
                table: "AccountTypeSetting",
                columns: new[] { "Id", "AccountGroupId", "AccountType", "AccountTypeTitle", "CardPrice", "CardSendPrice", "CardToAccount", "IdentificationInquiry", "InqueryPrice", "IsActive", "MaxAge", "MinAge", "MinBalance", "PostalCodeInquiry", "Stamp", "SysDate" },
                values: new object[] { (short)3, "000", (byte)2, "مرابحه", 36000L, 250000L, 19800L, 4500L, 0L, true, (byte)70, (byte)18, 0L, 15000L, 10000, new DateTime(2023, 8, 20, 15, 41, 36, 433, DateTimeKind.Local).AddTicks(1390) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DropColumn(
                name: "CardToAccount",
                table: "AccountTypeSetting");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 8, 16, 8, 56, 18, 411, DateTimeKind.Local).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 8, 16, 8, 56, 18, 411, DateTimeKind.Local).AddTicks(9426));

            migrationBuilder.InsertData(
                table: "AccountTypeSetting",
                columns: new[] { "Id", "AccountGroupId", "AccountType", "AccountTypeTitle", "CardPrice", "CardSendPrice", "IdentificationInquiry", "InqueryPrice", "IsActive", "MaxAge", "MinAge", "MinBalance", "PostalCodeInquiry", "Stamp", "SysDate" },
                values: new object[] { (short)4, "000", (byte)2, "مرابحه", 36000L, 250000L, 4500L, 0L, true, (byte)70, (byte)18, 500000L, 15000L, 10000, new DateTime(2023, 8, 16, 8, 56, 18, 411, DateTimeKind.Local).AddTicks(9444) });
        }
    }
}
