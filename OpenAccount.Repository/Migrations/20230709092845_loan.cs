using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class loan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SamatLoanInquiryRequest_Request_Id",
                table: "SamatLoanInquiryRequest");

            migrationBuilder.AlterColumn<string>(
                name: "ActionCode",
                table: "SamatLoanInquiryRequest",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Response code of service",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ErrorExMessage",
                table: "SamatLoanInquiryRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "خطای سیستمی");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "SamatLoanInquiryRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "درخواست افتتاح حساب");

            migrationBuilder.AddColumn<DateTime>(
                name: "SysDate",
                table: "SamatLoanInquiryRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                comment: "درخواست افتتاح حساب",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "استعلام تسهیلات سمات");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 9, 12, 58, 45, 559, DateTimeKind.Local).AddTicks(6846));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 9, 12, 58, 45, 559, DateTimeKind.Local).AddTicks(6914));

            migrationBuilder.CreateIndex(
                name: "IX_SamatLoanInquiryRequest_RequestId",
                table: "SamatLoanInquiryRequest",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_SamatLoanInquiryRequest_Request_RequestId",
                table: "SamatLoanInquiryRequest",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SamatLoanInquiryRequest_Request_RequestId",
                table: "SamatLoanInquiryRequest");

            migrationBuilder.DropIndex(
                name: "IX_SamatLoanInquiryRequest_RequestId",
                table: "SamatLoanInquiryRequest");

            migrationBuilder.DropColumn(
                name: "ErrorExMessage",
                table: "SamatLoanInquiryRequest");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "SamatLoanInquiryRequest");

            migrationBuilder.DropColumn(
                name: "SysDate",
                table: "SamatLoanInquiryRequest");

            migrationBuilder.AlterColumn<string>(
                name: "ActionCode",
                table: "SamatLoanInquiryRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Response code of service");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                comment: "استعلام تسهیلات سمات",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "درخواست افتتاح حساب");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 3, 11, 17, 16, 99, DateTimeKind.Local).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 3, 11, 17, 16, 99, DateTimeKind.Local).AddTicks(8314));

            migrationBuilder.AddForeignKey(
                name: "FK_SamatLoanInquiryRequest_Request_Id",
                table: "SamatLoanInquiryRequest",
                column: "Id",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
