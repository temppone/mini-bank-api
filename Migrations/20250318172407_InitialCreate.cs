using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "DailyTransactions",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTransactions", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DestinationAccountIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceAccountIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    DailyTransactionsIdentifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Transactions_DailyTransactions_DailyTransactionsIdentifier",
                        column: x => x.DailyTransactionsIdentifier,
                        principalTable: "DailyTransactions",
                        principalColumn: "Identifier");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DailyTransactionsIdentifier",
                table: "Transactions",
                column: "DailyTransactionsIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "DailyTransactions");
        }
    }
}
