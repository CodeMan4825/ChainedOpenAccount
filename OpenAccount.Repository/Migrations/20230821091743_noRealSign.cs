using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
    /// <inheritdoc />
    public partial class noRealSign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestRealSignature");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestRealSignature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "بایگانی پاراف خیس"),
                    ArchiveError = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "نتیجه ی بایگانی"),
                    SignatureArchived = table.Column<bool>(type: "bit", nullable: false, comment: "آیا پاراف بدرستی بایگانی شد؟"),
                    SignatureFileName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Signature file name in minIo"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ارسال پاراف")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRealSignature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRealSignature_Request_Id",
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
                value: new DateTime(2023, 8, 20, 15, 41, 36, 433, DateTimeKind.Local).AddTicks(1326));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 8, 20, 15, 41, 36, 433, DateTimeKind.Local).AddTicks(1366));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "SysDate",
                value: new DateTime(2023, 8, 20, 15, 41, 36, 433, DateTimeKind.Local).AddTicks(1390));
        }
    }
}
