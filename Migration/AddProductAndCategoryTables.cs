using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAPI.Infra.Data.Migrations
{
    public partial class AddProductAndCategoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criação da tabela 'Categories'
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false), // A chave primária vem da herança
                    Name = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            // Criação da tabela 'Products'
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HasStock = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Name);

                    // Definindo a chave estrangeira para 'Category'
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Criação de índice para 'CategoryId'
            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover a tabela 'Products'
            migrationBuilder.DropTable(
                name: "Products");

            // Remover a tabela 'Categories'
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}