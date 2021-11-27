using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace account_ms.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    idClient = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fName = table.Column<string>(type: "varChar(150)", nullable: false),
                    sName = table.Column<string>(type: "varChar(150)", nullable: true),
                    sureName = table.Column<string>(type: "varChar(150)", nullable: false),
                    Active = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "varChar(150)", nullable: false),
                    telNumber = table.Column<long>(type: "bigint", nullable: false),
                    password = table.Column<string>(type: "varChar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.idClient);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    idCard = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idClient = table.Column<int>(type: "integer", nullable: false),
                    cardNumber = table.Column<long>(type: "bigint", nullable: false),
                    dueDate = table.Column<string>(type: "varChar(150)", nullable: false),
                    cvv = table.Column<string>(type: "varChar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.idCard);
                    table.ForeignKey(
                        name: "FK_CreditCard_Client_idClient",
                        column: x => x.idClient,
                        principalTable: "Client",
                        principalColumn: "idClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_idClient",
                table: "CreditCard",
                column: "idClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
