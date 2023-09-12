using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopCart.Migrations
{
    /// <inheritdoc />
    public partial class interview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsbnNumber = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBooks",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    BookId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBooks", x => new { x.OrderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_OrderBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBooks_Books_BookId1",
                        column: x => x.BookId1,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderBooks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "IsbnNumber", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7, this latest edition of our guide will get you coding in C# with confidence.\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, implementing interfaces, and inheriting classes. Next, you'll take on .NET APIs for performing tasks like managing and querying data, working with the filesystem, and serialization. As you progress, you'll also explore examples of cross-platform projects you can build and deploy, such as websites and services using ASP.NET Core", "978-1803237800", 200m, "C# 11 and .NET 7" },
                    { 2, "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7", "978-1803237800", 400m, ".NET 7 book" },
                    { 3, "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7,this latest edition of our guide will get you coding in C# with confidence.", "978-1803237800", 200m, "C# 11 book" },
                    { 4, "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7, this latest edition of our guide will get you coding in C# with confidence.\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, ", "978-1803237800", 200m, "C# book" },
                    { 5, "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7, this latest edition of our guide will get you coding in C# with confidence.\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, implementing interfaces, and inheriting classes.", "978-1803237800", 400m, ".Net Book " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBooks_BookId",
                table: "OrderBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBooks_BookId1",
                table: "OrderBooks",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
