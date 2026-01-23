using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFeildName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiledDate",
                table: "UserSocial",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiledDate",
                table: "User",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiledDate",
                table: "Message",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiledDate",
                table: "Friend",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiledDate",
                table: "Conversation",
                newName: "ModifiedDate");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "UserSocial",
                newName: "ModifiledDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "User",
                newName: "ModifiledDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Message",
                newName: "ModifiledDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Friend",
                newName: "ModifiledDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Conversation",
                newName: "ModifiledDate");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                columns: new[] { "CreatedDate", "ModifiledDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                columns: new[] { "CreatedDate", "ModifiledDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                columns: new[] { "CreatedDate", "ModifiledDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
