using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PessengerApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessengers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Surname = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DocumentType = table.Column<int>(nullable: false),
                    DocumentNo = table.Column<string>(maxLength: 20, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessengers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pessengers",
                columns: new[] { "Id", "Country", "DocumentNo", "DocumentType", "Gender", "IssueDate", "Name", "Status", "Surname" },
                values: new object[,]
                {
                    { 403101, 2, "PS01203", 0, 0, new DateTime(2018, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kerem", 0, "Tunç" },
                    { 403102, 2, "PS01415", 0, 1, new DateTime(2015, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emine", 0, "Özdemir" },
                    { 403103, 1, "TD03156", 2, 0, new DateTime(2021, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yasin", 0, "Şahin" },
                    { 403104, 1, "VS202113", 1, 1, new DateTime(2021, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Erva", 0, "Tunç" },
                    { 403105, 2, "VS202147", 1, 1, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayşe", 1, "Uzun" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessengers");
        }
    }
}
