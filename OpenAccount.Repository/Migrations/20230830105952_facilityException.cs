using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
    /// <inheritdoc />
    public partial class facilityException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "StartException",
                oldComment: "خطا های مرحله ی اولیه");

            migrationBuilder.AlterTable(
                name: "EntityException",
                oldComment: "خطا های موجودیت");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "StartException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطاهای اعتبارسنجی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "EntityException",
                type: "uniqueidentifier",
                nullable: false,
                comment: "خطاهای اعتبارسنجی",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "InquiryException",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "خطاهای اعتبارسنجی"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "درخواست افتتاح حساب")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryException", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryException_EntityException_Id",
                        column: x => x.Id,
                        principalTable: "EntityException",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryException_Request_RequestId",
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

            migrationBuilder.CreateIndex(
                name: "IX_InquiryException_RequestId",
                table: "InquiryException",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InquiryException");

            migrationBuilder.AlterTable(
                name: "StartException",
                comment: "خطا های مرحله ی اولیه");

            migrationBuilder.AlterTable(
                name: "EntityException",
                comment: "خطا های موجودیت");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "StartException",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطاهای اعتبارسنجی");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "EntityException",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "خطاهای اعتبارسنجی");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 8, 21, 12, 47, 43, 335, DateTimeKind.Local).AddTicks(1077));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 8, 21, 12, 47, 43, 335, DateTimeKind.Local).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "SysDate",
                value: new DateTime(2023, 8, 21, 12, 47, 43, 335, DateTimeKind.Local).AddTicks(1137));
        }
    }
}
