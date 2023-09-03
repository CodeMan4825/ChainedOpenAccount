using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class signer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitalSignatureDone",
                table: "RequestDigitalSignature");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SysDate",
                table: "RequestDigitalSignature",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ اعمال شدن امضاء",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ دریافت فایل");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی امضاء دیجیتال کاربر");

            migrationBuilder.AddColumn<string>(
                name: "SignatureSent",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی امضاء دیجیتال کاربر ارسال شد");

            migrationBuilder.AddColumn<string>(
                name: "SignerCertificate",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی امضاء جهت ارسال اولیه");

            migrationBuilder.AddColumn<bool>(
                name: "SignerCertificateDone",
                table: "RequestDigitalSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "گواهی امضاء اولیه دریافت شد؟");

            migrationBuilder.AddColumn<string>(
                name: "SignerCertificateResult",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "نتیجه ی گواهی امضاء اولیه");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "SignatureSent",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "SignerCertificate",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "SignerCertificateDone",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "SignerCertificateResult",
                table: "RequestDigitalSignature");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SysDate",
                table: "RequestDigitalSignature",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ دریافت فایل",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ اعمال شدن امضاء");

            migrationBuilder.AddColumn<bool>(
                name: "DigitalSignatureDone",
                table: "RequestDigitalSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "امضای دیجیتال اعمال شد؟");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 16, 14, 6, 17, 649, DateTimeKind.Local).AddTicks(4436));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 16, 14, 6, 17, 649, DateTimeKind.Local).AddTicks(4478));
        }
    }
}
