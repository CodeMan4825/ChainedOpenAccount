using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class realsigntwice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendToBankMessage",
                table: "RequestRealSignature");

            migrationBuilder.DropColumn(
                name: "SignatureSentToBank",
                table: "RequestRealSignature");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RequestRealSignature",
                type: "uniqueidentifier",
                nullable: false,
                comment: "بایگانی پاراف خیس",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "پاراف خیس");

            migrationBuilder.CreateTable(
                name: "RequestRealSignatureToBank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ارسال پاراف خیس به بانک"),
                    SignatureSentToBank = table.Column<bool>(type: "bit", nullable: false, comment: "آیا پاراف برای بانک بدرستی ارسال شد؟"),
                    SendToBankMessage = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "نتیجه ی ارسال پاراف"),
                    SysDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ارسال پاراف")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRealSignatureToBank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRealSignatureToBank_Request_Id",
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
                value: new DateTime(2023, 7, 23, 15, 43, 37, 570, DateTimeKind.Local).AddTicks(6677));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 23, 15, 43, 37, 570, DateTimeKind.Local).AddTicks(6724));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestRealSignatureToBank");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RequestRealSignature",
                type: "uniqueidentifier",
                nullable: false,
                comment: "پاراف خیس",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "بایگانی پاراف خیس");

            migrationBuilder.AddColumn<string>(
                name: "SendToBankMessage",
                table: "RequestRealSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "نتیجه ی ارسال پاراف");

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
    }
}
