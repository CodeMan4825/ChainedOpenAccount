using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class noCoomitment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestCommitmentLetter");

            migrationBuilder.DropColumn(
                name: "SignatureSent",
                table: "RequestDigitalSignature");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RequestDigitalSignatureLog",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطاهای امضای دیجیتال هر درخواست",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RequestDigitalSignatureLog",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطاهای امضای دیجیتال هر درخواست");

            migrationBuilder.AddColumn<bool>(
                name: "SignatureSent",
                table: "RequestDigitalSignature",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "گواهی امضاء دیجیتال کاربر ارسال شد؟");

            migrationBuilder.CreateTable(
                name: "RequestCommitmentLetter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "دریافت تعهدنامه"),
                    CommitmentLetterError = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "مشکل دریافت تعهدنامه"),
                    CommitmentLetterGot = table.Column<bool>(type: "bit", nullable: false, comment: "تعهدنامه دریافت شد؟"),
                    FileAddress = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "آدرس فایل دریافت شده"),
                    FileNameInFileManager = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نام جدید فایلی که سرویس مدیریت فایل ها ایجاد کرده"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ دریافت فایل")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCommitmentLetter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCommitmentLetter_Request_Id",
                        column: x => x.Id,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 31, 54, 663, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 31, 54, 663, DateTimeKind.Local).AddTicks(5267));
        }
    }
}
