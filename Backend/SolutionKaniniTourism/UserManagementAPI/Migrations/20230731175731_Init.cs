using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HashKey = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Details_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelAgents",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AgencyPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AgencyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelAgents", x => x.Email);
                    table.ForeignKey(
                        name: "FK_TravelAgents_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "HashKey", "Id", "Password", "Role", "Status" },
                values: new object[] { "Admin@gmail.com", new byte[] { 136, 74, 215, 45, 125, 125, 134, 118, 105, 106, 183, 63, 15, 38, 143, 70, 70, 228, 217, 184, 54, 70, 226, 60, 230, 218, 176, 159, 12, 219, 53, 89 }, 1, new byte[] { 214, 103, 110, 34, 133, 85, 194, 4, 199, 18, 128, 177, 107, 236, 148, 16, 196, 92, 18, 49, 71, 81, 155, 189, 58, 222, 173, 15, 77, 202, 192, 230, 174, 168, 173, 175, 152, 0, 185, 80, 191, 252, 86, 232, 2, 99, 116, 39, 94, 196, 84, 202, 99, 20, 40, 104, 135, 17, 43, 32, 165, 119, 123, 26 }, "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "TravelAgents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
