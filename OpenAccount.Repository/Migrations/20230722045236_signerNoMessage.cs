using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class signerNoMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DigitalSignatureError",
                table: "RequestDigitalSignature");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 22, 35, 957, DateTimeKind.Local).AddTicks(7834));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 22, 8, 22, 35, 957, DateTimeKind.Local).AddTicks(7875));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DigitalSignatureError",
                table: "RequestDigitalSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "مشکل امضای دیجیتال");

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
        }
    }
}
