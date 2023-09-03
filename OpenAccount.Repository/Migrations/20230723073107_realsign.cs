using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class realsign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileAddress",
                table: "RequestRealSignature");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SysDate",
                table: "RequestRealSignature",
                type: "datetime2",
                nullable: false,
                comment: "زمان ارسال پاراف",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ دریافت فایل");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RequestRealSignature",
                type: "uniqueidentifier",
                nullable: false,
                comment: "پاراف خیس",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "امضای وافعی");

            migrationBuilder.AddColumn<string>(
                name: "ArchiveError",
                table: "RequestRealSignature",
                type: "nvarchar(max)",
                nullable: true,
                comment: "نتیجه ی بایگانی");

            migrationBuilder.AddColumn<string>(
                name: "SendToBankMessage",
                table: "RequestRealSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "نتیجه ی ارسال پاراف");

            migrationBuilder.AddColumn<bool>(
                name: "SignatureArchived",
                table: "RequestRealSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "آیا پاراف بدرستی بایگانی شد؟");

            migrationBuilder.AddColumn<string>(
                name: "SignatureFileName",
                table: "RequestRealSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Signature file name in minIo");

            migrationBuilder.AddColumn<bool>(
                name: "SignatureSentToBank",
                table: "RequestRealSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "آیا پاراف برای بانک بدرستی ارسال شد؟");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 23, 11, 1, 7, 229, DateTimeKind.Local).AddTicks(3658));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 23, 11, 1, 7, 229, DateTimeKind.Local).AddTicks(3693));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveError",
                table: "RequestRealSignature");

            migrationBuilder.DropColumn(
                name: "SendToBankMessage",
                table: "RequestRealSignature");

            migrationBuilder.DropColumn(
                name: "SignatureArchived",
                table: "RequestRealSignature");

            migrationBuilder.DropColumn(
                name: "SignatureFileName",
                table: "RequestRealSignature");

            migrationBuilder.DropColumn(
                name: "SignatureSentToBank",
                table: "RequestRealSignature");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SysDate",
                table: "RequestRealSignature",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ دریافت فایل",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "زمان ارسال پاراف");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RequestRealSignature",
                type: "uniqueidentifier",
                nullable: false,
                comment: "امضای وافعی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "پاراف خیس");

            migrationBuilder.AddColumn<string>(
                name: "FileAddress",
                table: "RequestRealSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "آدرس فایل دریافت شده");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 10, 55, 36, 784, DateTimeKind.Local).AddTicks(5449));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 10, 55, 36, 784, DateTimeKind.Local).AddTicks(5479));
        }
    }
}
