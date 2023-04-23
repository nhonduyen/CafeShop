using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeShop.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsOwner = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiredDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    OrderNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Date = table.Column<long>(type: "bigint", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    SubTotal = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    TotalBill = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Table_TableId",
                        column: x => x.TableId,
                        principalSchema: "public",
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "public",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "public",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_MerchantId",
                schema: "public",
                table: "Category",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_MerchantId_IsDelete",
                schema: "public",
                table: "Category",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_MerchantId_Name",
                schema: "public",
                table: "Category",
                columns: new[] { "MerchantId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MerchantId",
                schema: "public",
                table: "Employee",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MerchantId_IsDelete",
                schema: "public",
                table: "Employee",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_MerchantId",
                schema: "public",
                table: "Image",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_MerchantId_IsDelete",
                schema: "public",
                table: "Image",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_MerchantId_ItemType",
                schema: "public",
                table: "Image",
                columns: new[] { "MerchantId", "ItemType" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_MerchantId_ItemType_ItemId",
                schema: "public",
                table: "Image",
                columns: new[] { "MerchantId", "ItemType", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_Code",
                schema: "public",
                table: "Merchant",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_MerchantId",
                schema: "public",
                table: "Order",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MerchantId_IsDelete",
                schema: "public",
                table: "Order",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_MerchantId_OrderNo",
                schema: "public",
                table: "Order",
                columns: new[] { "MerchantId", "OrderNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_MerchantId_TableId",
                schema: "public",
                table: "Order",
                columns: new[] { "MerchantId", "TableId" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_TableId",
                schema: "public",
                table: "Order",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MerchantId",
                schema: "public",
                table: "OrderDetail",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MerchantId_IsDelete",
                schema: "public",
                table: "OrderDetail",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MerchantId_OrderId",
                schema: "public",
                table: "OrderDetail",
                columns: new[] { "MerchantId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MerchantId_ProductId",
                schema: "public",
                table: "OrderDetail",
                columns: new[] { "MerchantId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                schema: "public",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                schema: "public",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "public",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MerchantId",
                schema: "public",
                table: "Product",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MerchantId_Code",
                schema: "public",
                table: "Product",
                columns: new[] { "MerchantId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_MerchantId_IsDelete",
                schema: "public",
                table: "Product",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_Table_MerchantId",
                schema: "public",
                table: "Table",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_MerchantId_IsDelete",
                schema: "public",
                table: "Table",
                columns: new[] { "MerchantId", "IsDelete" });

            migrationBuilder.CreateIndex(
                name: "IX_Table_MerchantId_Name",
                schema: "public",
                table: "Table",
                columns: new[] { "MerchantId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Merchant",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OrderDetail",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Table",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "public");
        }
    }
}
