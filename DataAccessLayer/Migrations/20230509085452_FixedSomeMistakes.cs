using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixedSomeMistakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KCalorie = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServingSize = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TotalFat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SaturatedFat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Carbohydrates = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Protein = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Recipe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyUsersInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KCalorieReal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KCalorieGoal = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUsersInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyUsersInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyUserInfoDish",
                columns: table => new
                {
                    DailyUsersInfoId = table.Column<int>(type: "int", nullable: false),
                    DishesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUserInfoDish", x => new { x.DailyUsersInfoId, x.DishesId });
                    table.ForeignKey(
                        name: "FK_DailyUserInfoDish_DailyUsersInfo_DailyUsersInfoId",
                        column: x => x.DailyUsersInfoId,
                        principalTable: "DailyUsersInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyUserInfoDish_Dishes_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyUserInfoDish_DishesId",
                table: "DailyUserInfoDish",
                column: "DishesId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyUsersInfo_Id",
                table: "DailyUsersInfo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DailyUsersInfo_UserId",
                table: "DailyUsersInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_Id",
                table: "Dishes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyUserInfoDish");

            migrationBuilder.DropTable(
                name: "DailyUsersInfo");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
