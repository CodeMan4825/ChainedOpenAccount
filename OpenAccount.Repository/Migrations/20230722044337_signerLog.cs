using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class signerLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
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

            migrationBuilder.AddColumn<string>(
                name: "FinalDigest",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی پایانی");

            migrationBuilder.AddColumn<string>(
                name: "FirstDigest",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی نخست");

            migrationBuilder.AddColumn<bool>(
                name: "PdfGenerated",
                table: "RequestDigitalSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "فایل از روی داده های کاربر درست و بایگانی شد");

            migrationBuilder.AddColumn<bool>(
                name: "PdfSignedByBank",
                table: "RequestDigitalSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "فایل بوسیله ی بانک امضاء شد؟");

            migrationBuilder.AddColumn<string>(
                name: "RootCertification",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی ریشه");

            migrationBuilder.AddColumn<string>(
                name: "SignGeneratedByApp",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی تولید شده بوسیله ی برنامه جانبی");

            migrationBuilder.AddColumn<byte[]>(
                name: "TempPdfFile",
                table: "RequestDigitalSignature",
                type: "varbinary(max)",
                nullable: true,
                comment: "فایل تولید شده تا امضای طرفین");

            migrationBuilder.AddColumn<string>(
                name: "FileNameInFileManager",
                table: "RequestCommitmentLetter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "نام جدید فایلی که سرویس مدیریت فایل ها ایجاد کرده");

            migrationBuilder.CreateTable(
                name: "RequestDigitalSignatureLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestDigitalSignatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "امضای دیجیتال هر درخواست"),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "متن خطا"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان وقوع خطا")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDigitalSignatureLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDigitalSignatureLog_RequestDigitalSignature_RequestDigitalSignatureId",
                        column: x => x.RequestDigitalSignatureId,
                        principalTable: "RequestDigitalSignature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 13, 37, 74, DateTimeKind.Local).AddTicks(3162));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 13, 37, 74, DateTimeKind.Local).AddTicks(3195));

            migrationBuilder.CreateIndex(
                name: "IX_RequestDigitalSignatureLog_RequestDigitalSignatureId",
                table: "RequestDigitalSignatureLog",
                column: "RequestDigitalSignatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestDigitalSignatureLog");

            migrationBuilder.DropColumn(
                name: "FinalDigest",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "FirstDigest",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "PdfGenerated",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "PdfSignedByBank",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "RootCertification",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "SignGeneratedByApp",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "TempPdfFile",
                table: "RequestDigitalSignature");

            migrationBuilder.DropColumn(
                name: "FileNameInFileManager",
                table: "RequestCommitmentLetter");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "گواهی امضاء دیجیتال کاربر");

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
                value: new DateTime(2023, 7, 19, 11, 56, 23, 510, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 19, 11, 56, 23, 510, DateTimeKind.Local).AddTicks(9233));
        }
    }
}
