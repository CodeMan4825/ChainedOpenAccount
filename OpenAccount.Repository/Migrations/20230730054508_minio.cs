using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class minio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileNameInDms",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "نام فایل در سیستم مدیریت فایل");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 30, 9, 15, 7, 946, DateTimeKind.Local).AddTicks(180));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 30, 9, 15, 7, 946, DateTimeKind.Local).AddTicks(215));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNameInDms",
                table: "RequestDigitalSignature");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 25, 16, 37, 34, 708, DateTimeKind.Local).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 25, 16, 37, 34, 708, DateTimeKind.Local).AddTicks(7676));
        }
    }
}
