using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace etherscan_test.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "int(20)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BlockNumber = table.Column<int>(type: "int(20)", nullable: false),
                    Hash = table.Column<string>(type: "varchar(66)", nullable: true),
                    ParentHash = table.Column<string>(type: "varchar(66)", nullable: true),
                    Miner = table.Column<string>(type: "varchar(42)", nullable: true),
                    BlockReward = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    GasLimit = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    GasUsed = table.Column<decimal>(type: "decimal(50,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.BlockId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int(20)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BlockId = table.Column<int>(type: "int(20)", nullable: false),
                    Hash = table.Column<string>(type: "varchar(66)", nullable: false),
                    From = table.Column<string>(type: "varchar(42)", nullable: false),
                    To = table.Column<string>(type: "varchar(42)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    Gas = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    GasPrice = table.Column<decimal>(type: "decimal(50,0)", nullable: false),
                    TransactionIndex = table.Column<int>(type: "int(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Block_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Block",
                        principalColumn: "BlockId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BlockId",
                table: "Transaction",
                column: "BlockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Block");
        }
    }
}
