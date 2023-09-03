using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class signer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SignatureSent",
                table: "RequestDigitalSignature",
                type: "bit",
                nullable: false,
                comment: "گواهی امضاء دیجیتال کاربر ارسال شد؟",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "گواهی امضاء دیجیتال کاربر ارسال شد");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 19, 11, 56, 23, 510, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 19, 11, 56, 23, 510, DateTimeKind.Local).AddTicks(9233));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SignatureSent",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                comment: "گواهی امضاء دیجیتال کاربر ارسال شد",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "گواهی امضاء دیجیتال کاربر ارسال شد؟");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 19, 11, 53, 6, 641, DateTimeKind.Local).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 19, 11, 53, 6, 641, DateTimeKind.Local).AddTicks(4968));
        }
    }
}
