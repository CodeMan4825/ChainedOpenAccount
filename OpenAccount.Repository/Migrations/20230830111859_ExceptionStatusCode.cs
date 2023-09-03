using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ExceptionStatusCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "EntityException",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Status code");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 8, 30, 14, 48, 58, 661, DateTimeKind.Local).AddTicks(7442));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 8, 30, 14, 48, 58, 661, DateTimeKind.Local).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "SysDate",
                value: new DateTime(2023, 8, 30, 14, 48, 58, 661, DateTimeKind.Local).AddTicks(7506));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "EntityException");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 8, 30, 14, 29, 52, 131, DateTimeKind.Local).AddTicks(8230));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 8, 30, 14, 29, 52, 131, DateTimeKind.Local).AddTicks(8319));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "SysDate",
                value: new DateTime(2023, 8, 30, 14, 29, 52, 131, DateTimeKind.Local).AddTicks(8344));
        }
    }
}
