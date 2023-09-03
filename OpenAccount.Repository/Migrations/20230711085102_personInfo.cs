using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class personInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInfo_Person_PersonId",
                table: "PersonInfo");

            migrationBuilder.DropIndex(
                name: "IX_PersonInfo_PersonId",
                table: "PersonInfo");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "RealPersonid",
                table: "RealPersonInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "شخص حقیقی");

            migrationBuilder.AddColumn<Guid>(
                name: "LegalPersonid",
                table: "LegalPersonInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "شخص حقوقی");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 11, 12, 21, 2, 389, DateTimeKind.Local).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 11, 12, 21, 2, 389, DateTimeKind.Local).AddTicks(5794));

            migrationBuilder.CreateIndex(
                name: "IX_RealPersonInfo_RealPersonid",
                table: "RealPersonInfo",
                column: "RealPersonid");

            migrationBuilder.CreateIndex(
                name: "IX_LegalPersonInfo_LegalPersonid",
                table: "LegalPersonInfo",
                column: "LegalPersonid");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalPersonInfo_LegalPerson_LegalPersonid",
                table: "LegalPersonInfo",
                column: "LegalPersonid",
                principalTable: "LegalPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealPersonInfo_RealPerson_RealPersonid",
                table: "RealPersonInfo",
                column: "RealPersonid",
                principalTable: "RealPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalPersonInfo_LegalPerson_LegalPersonid",
                table: "LegalPersonInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_RealPersonInfo_RealPerson_RealPersonid",
                table: "RealPersonInfo");

            migrationBuilder.DropIndex(
                name: "IX_RealPersonInfo_RealPersonid",
                table: "RealPersonInfo");

            migrationBuilder.DropIndex(
                name: "IX_LegalPersonInfo_LegalPersonid",
                table: "LegalPersonInfo");

            migrationBuilder.DropColumn(
                name: "RealPersonid",
                table: "RealPersonInfo");

            migrationBuilder.DropColumn(
                name: "LegalPersonid",
                table: "LegalPersonInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "PersonInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "شخص");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 10, 7, 32, 47, 774, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 10, 7, 32, 47, 774, DateTimeKind.Local).AddTicks(3874));

            migrationBuilder.CreateIndex(
                name: "IX_PersonInfo_PersonId",
                table: "PersonInfo",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInfo_Person_PersonId",
                table: "PersonInfo",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
