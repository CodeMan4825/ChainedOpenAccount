using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
    /// <inheritdoc />
    public partial class personException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "StartException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطا های مراحل اطلاعات پرسنلی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطاهای اعتبارسنجی");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "InquiryException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطا های مراحل اطلاعات پرسنلی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطاهای اعتبارسنجی");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "EntityException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطا های مراحل اطلاعات پرسنلی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطاهای اعتبارسنجی");

            migrationBuilder.CreateTable(
                name: "PersonException",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "خطا های مراحل اطلاعات پرسنلی"),
                    PersonState = table.Column<byte>(type: "tinyint", nullable: false),
                    PersonStateCaption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonException", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonException_EntityException_Id",
                        column: x => x.Id,
                        principalTable: "EntityException",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonException_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 9, 2, 14, 32, 25, 207, DateTimeKind.Local).AddTicks(996));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 9, 2, 14, 32, 25, 207, DateTimeKind.Local).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "SysDate",
                value: new DateTime(2023, 9, 2, 14, 32, 25, 207, DateTimeKind.Local).AddTicks(1056));

            migrationBuilder.CreateIndex(
                name: "IX_PersonException_RequestId",
                table: "PersonException",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonException");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "StartException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطاهای اعتبارسنجی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطا های مراحل اطلاعات پرسنلی");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "InquiryException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطاهای اعتبارسنجی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطا های مراحل اطلاعات پرسنلی");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "EntityException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطاهای اعتبارسنجی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطا های مراحل اطلاعات پرسنلی");

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
    }
}
