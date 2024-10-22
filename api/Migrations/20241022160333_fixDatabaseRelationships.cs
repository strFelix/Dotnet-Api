using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class fixDatabaseRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CorrectOption = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    OptionNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => new { x.AnswerId, x.OptionNumber });
                    table.ForeignKey(
                        name: "FK_options_answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "responses",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_responses", x => new { x.UserId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_responses_answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_responses_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "answers",
                columns: new[] { "Id", "CorrectOption", "Title" },
                values: new object[,]
                {
                    { 1, 3, "What is a correct syntax to output 'Hello World' in C#?" },
                    { 2, 4, "Which data type is used to create a variable that should store text?" }
                });

            migrationBuilder.InsertData(
                table: "options",
                columns: new[] { "AnswerId", "OptionNumber", "Description" },
                values: new object[,]
                {
                    { 1, 1, "print('Hello World')" },
                    { 1, 2, "cout < < 'Hello World'" },
                    { 1, 3, "System.out.printLn('Hello World')" },
                    { 1, 4, "Console.WriteLine('Hello World')" },
                    { 2, 1, "myString" },
                    { 2, 2, "Txt" },
                    { 2, 3, "str" },
                    { 2, 4, "string" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_Title",
                table: "answers",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_responses_AnswerId",
                table: "responses",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "responses");

            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
