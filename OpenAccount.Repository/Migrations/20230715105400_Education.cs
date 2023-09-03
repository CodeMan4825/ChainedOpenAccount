using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAccount.Repository.Migrations
{
	/// <inheritdoc />
	public partial class Education : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealPerson_Education_EducationId",
                table: "RealPerson");

            migrationBuilder.DropIndex(
                name: "IX_RealPerson_EducationId",
                table: "RealPerson");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "RealPerson");

            migrationBuilder.AddColumn<byte>(
                name: "EducationId",
                table: "RealPersonInfo",
                type: "tinyint",
                nullable: true,
                comment: "تحصیلات");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 14, 24, 0, 380, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 14, 24, 0, 380, DateTimeKind.Local).AddTicks(3137));

            migrationBuilder.CreateIndex(
                name: "IX_RealPersonInfo_EducationId",
                table: "RealPersonInfo",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealPersonInfo_Education_EducationId",
                table: "RealPersonInfo",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealPersonInfo_Education_EducationId",
                table: "RealPersonInfo");

            migrationBuilder.DropIndex(
                name: "IX_RealPersonInfo_EducationId",
                table: "RealPersonInfo");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "RealPersonInfo");

            migrationBuilder.AddColumn<byte>(
                name: "EducationId",
                table: "RealPerson",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "تحصیلات");

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 14, 18, 22, 613, DateTimeKind.Local).AddTicks(6815));

            migrationBuilder.UpdateData(
                table: "AccountTypeSetting",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "SysDate",
                value: new DateTime(2023, 7, 15, 14, 18, 22, 613, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.CreateIndex(
                name: "IX_RealPerson_EducationId",
                table: "RealPerson",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealPerson_Education_EducationId",
                table: "RealPerson",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
