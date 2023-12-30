using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myafim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterDateTimeOffsetToBinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ValueDate",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ValueDate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }
    }
}
