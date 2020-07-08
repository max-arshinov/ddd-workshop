using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNext.DddWorkshop.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Products",
                new[] {"Name", "Price", "DiscountPercent"},
                new object[] { "iPhone XR", 599, 0}
            );
            
            migrationBuilder.InsertData(
                "Products",
                new[] {"Name", "Price", "DiscountPercent"},
                new object[] { "MacBook Pro 16", 2790, 0}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
