using System;
using Microsoft.EntityFrameworkCore.Migrations;
using fStore.Core;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fStore.WEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_status", "pending,processing,cancelled,shipped,delivered")
                .Annotation("Npgsql:Enum:role", "customer,admin");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<Role>(type: "role", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    inventory = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.CheckConstraint("CK_Product_Inventory_Positive", "inventory>=0");
                    table.CheckConstraint("CK_Product_Price_Positive", "price>=0");
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    house_number = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    post_code = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_status = table.Column<OrderStatus>(type: "order_status", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    review_text = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders_products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quntity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders_products", x => new { x.order_id, x.product_id });
                    table.CheckConstraint("CK_OrderProduct_Quntity_Positive", "quntity>=0");
                    table.CheckConstraint("CK_OrderProduct_TotalPrice_Positive", "price>=0");
                    table.ForeignKey(
                        name: "fk_orders_products_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_at", "image_url", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4262), "https://api.lorem.space/image/book?w=150&h=220", "Books", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4263) },
                    { new Guid("3f518f2f-8979-4f00-a1a2-d9cb6758e5b0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4280), "https://i.imgur.com/BG8J0Fj.jpg", "Beauty", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4281) },
                    { new Guid("40d8506b-d6de-4e44-853c-681e496c3afe"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4270), "https://i.imgur.com/QkIa5tT.jpeg", "Home & Kitchen", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4272) },
                    { new Guid("5fadfac4-7607-4741-b3f9-c0519089b93e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4277), "https://i.imgur.com/ZANVnHE.jpeg", "Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4278) },
                    { new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4258), "https://i.imgur.com/Qphac99.jpeg", "Furniture", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4259) },
                    { new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4246), "https://i.imgur.com/QkIa5tT.jpeg", "Clothes", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4248) },
                    { new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4255), "https://i.imgur.com/ZANVnHE.jpeg", "Electronics", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4256) },
                    { new Guid("85edc972-1610-44ac-b3e2-dcda859224e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4274), "https://i.imgur.com/Qphac99.jpeg", "Health", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4275) },
                    { new Guid("9b2de812-fe5e-48b1-97c0-6383ece459e9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4265), "https://i.imgur.com/BG8J0Fj.jpg", "Sports", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4266) },
                    { new Guid("fdaf25e3-0edd-41fe-b300-156260f62d57"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4286), "https://i.imgur.com/Qphac99.jpeg", "Toys", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4287) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "avatar", "created_at", "email", "name", "password", "phone_number", "role", "salt", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0336ef81-7391-458d-a8ac-da0168e06d7c"), "https://picsum.photos/800", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3929), "kiara@mail.com", "kiara", "693D1D823ED2C1D8DAEF2307D3EBB43B9362D156D7C729326A3EF96E61037417", null, Role.Customer, new byte[] { 101, 156, 146, 119, 217, 180, 255, 88, 167, 60, 139, 194, 189, 166, 49, 185, 168, 193, 1, 15, 149, 108, 50, 142, 135, 36, 58, 116, 90, 179, 69, 220, 94, 234, 238, 50, 51, 236, 96, 188, 216, 193, 87, 52, 153, 207, 154, 135, 244, 35, 13, 105, 128, 88, 231, 18, 20, 225, 124, 18, 194, 217, 97, 174 }, new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3930) },
                    { new Guid("5efe4627-4e5c-4fe5-84a1-b8a2103abc7d"), "https://api.lorem.space/image/face?w=640&h=480&r=867", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3937), "maria@mail.com", "Maria", "693D1D823ED2C1D8DAEF2307D3EBB43B9362D156D7C729326A3EF96E61037417", null, Role.Customer, new byte[] { 101, 156, 146, 119, 217, 180, 255, 88, 167, 60, 139, 194, 189, 166, 49, 185, 168, 193, 1, 15, 149, 108, 50, 142, 135, 36, 58, 116, 90, 179, 69, 220, 94, 234, 238, 50, 51, 236, 96, 188, 216, 193, 87, 52, 153, 207, 154, 135, 244, 35, 13, 105, 128, 88, 231, 18, 20, 225, 124, 18, 194, 217, 97, 174 }, new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3938) },
                    { new Guid("80c55304-1bc4-4234-9684-bc3021973176"), "https://api.escuelajs.co/api/v1/files/8c21.jpg", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3933), "sara@mail.com", "Sara James", "693D1D823ED2C1D8DAEF2307D3EBB43B9362D156D7C729326A3EF96E61037417", null, Role.Customer, new byte[] { 101, 156, 146, 119, 217, 180, 255, 88, 167, 60, 139, 194, 189, 166, 49, 185, 168, 193, 1, 15, 149, 108, 50, 142, 135, 36, 58, 116, 90, 179, 69, 220, 94, 234, 238, 50, 51, 236, 96, 188, 216, 193, 87, 52, 153, 207, 154, 135, 244, 35, 13, 105, 128, 88, 231, 18, 20, 225, 124, 18, 194, 217, 97, 174 }, new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3934) },
                    { new Guid("82abe7e3-d76d-422b-841a-7acb4bbbca19"), "https://api.escuelajs.co/api/v1/files/4637.jpg", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3946), "smith@mail.com", "John Smith", "693D1D823ED2C1D8DAEF2307D3EBB43B9362D156D7C729326A3EF96E61037417", null, Role.Customer, new byte[] { 101, 156, 146, 119, 217, 180, 255, 88, 167, 60, 139, 194, 189, 166, 49, 185, 168, 193, 1, 15, 149, 108, 50, 142, 135, 36, 58, 116, 90, 179, 69, 220, 94, 234, 238, 50, 51, 236, 96, 188, 216, 193, 87, 52, 153, 207, 154, 135, 244, 35, 13, 105, 128, 88, 231, 18, 20, 225, 124, 18, 194, 217, 97, 174 }, new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3947) },
                    { new Guid("915801d0-e051-4074-aa8c-e189ee8d73be"), "https://i.imgur.com/LDOO4Qs.jpg", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3893), "admin@mail.com", "Admin", "B4EBED4C46D517B5E10CC88A3549246520A2BB004A052DFDE368C7727231ED2D", null, Role.Admin, new byte[] { 255, 31, 122, 50, 30, 83, 44, 179, 197, 164, 68, 131, 140, 176, 140, 35, 4, 235, 232, 120, 241, 230, 137, 172, 122, 81, 2, 9, 16, 116, 170, 42, 126, 239, 133, 243, 24, 171, 157, 112, 250, 171, 39, 65, 124, 222, 59, 101, 205, 96, 162, 177, 197, 248, 153, 121, 127, 70, 42, 27, 193, 18, 83, 203 }, new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3914) },
                    { new Guid("9fe0db88-1576-45b7-bb2a-6eba74e060f9"), "https://i.imgur.com/DTfowdu.jpg", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3925), "jane@mail.com", "Jane Doe", "693D1D823ED2C1D8DAEF2307D3EBB43B9362D156D7C729326A3EF96E61037417", null, Role.Customer, new byte[] { 101, 156, 146, 119, 217, 180, 255, 88, 167, 60, 139, 194, 189, 166, 49, 185, 168, 193, 1, 15, 149, 108, 50, 142, 135, 36, 58, 116, 90, 179, 69, 220, 94, 234, 238, 50, 51, 236, 96, 188, 216, 193, 87, 52, 153, 207, 154, 135, 244, 35, 13, 105, 128, 88, 231, 18, 20, 225, 124, 18, 194, 217, 97, 174 }, new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(3926) }
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "city", "country", "created_at", "house_number", "post_code", "street", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("0a15d06d-bb35-4b9b-a0c2-ec36697767da"), "Oulu", "Finland", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4416), "8 B", "90650", "Laamannintie", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4417), new Guid("5efe4627-4e5c-4fe5-84a1-b8a2103abc7d") },
                    { new Guid("13de2c23-770e-469d-bce9-010b535c608b"), "Oulu", "Finland", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4395), "A 1", "50610", "Peltokatu", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4397), new Guid("9fe0db88-1576-45b7-bb2a-6eba74e060f9") },
                    { new Guid("22ba55bd-d3e9-4631-bbc5-8785ced48bce"), "Oulu", "Finland", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4422), "3 A", "90650", "Laamannintie", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4423), new Guid("82abe7e3-d76d-422b-841a-7acb4bbbca19") },
                    { new Guid("99e538a8-dc23-4261-81e4-46bb41e3fc8c"), "Oulu", "Finland", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4411), "A 7", "90650", "Laamannintie", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4412), new Guid("80c55304-1bc4-4234-9684-bc3021973176") },
                    { new Guid("e28de71a-0635-4b04-8a15-3160d0538f35"), "Oulu", "Finland", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4405), "B 17", "50610", "Peltokatu", new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(4406), new Guid("0336ef81-7391-458d-a8ac-da0168e06d7c") }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "created_at", "description", "inventory", "price", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00a2d36b-38f9-4c89-8896-6ca91a7cde8d"), new Guid("85edc972-1610-44ac-b3e2-dcda859224e8"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(463), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 1, 44.55m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(464) },
                    { new Guid("00e4725f-3c80-4efd-84bc-d42586661139"), new Guid("5fadfac4-7607-4741-b3f9-c0519089b93e"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(660), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 30, 98.83m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(661) },
                    { new Guid("02904361-4464-481f-a033-d26330da5a2e"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9689), "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.", 79, 93.00m, "Digital Camera", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9690) },
                    { new Guid("02beea56-a18f-4405-a2d7-7e183cc6953d"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9591), "Etiam ullamcorper odio eu libero varius, eu feugiat dolor iaculis.", 49, 98.63m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9592) },
                    { new Guid("047f5e4c-3274-4e74-8ba5-cdebea954b9e"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9490), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 61, 4.98m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9491) },
                    { new Guid("0493cf0a-d7b0-405b-8146-f9233c19ee26"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9547), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 95, 25.36m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9548) },
                    { new Guid("05d0c36a-0fd0-4d81-83d7-67b5d96316fc"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(394), "Etiam ullamcorper odio eu libero varius, eu feugiat dolor iaculis.", 18, 80.93m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(394) },
                    { new Guid("091347ce-107e-444d-86ff-83e914824bbc"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9824), "Fusce eu libero eget arcu fermentum hendrerit id ut elit.", 62, 18.25m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9827) },
                    { new Guid("0bd1cdaf-f4bc-459a-a675-c0e86b5b3afb"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9577), "Proin vel tortor vel augue accumsan interdum id in justo.", 98, 0.19m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9578) },
                    { new Guid("0cdb8bd1-cf78-4d3c-94aa-ca8afe4b4319"), new Guid("fdaf25e3-0edd-41fe-b300-156260f62d57"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(106), "Sed ac odio eu orci luctus iaculis.", 20, 74.21m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(107) },
                    { new Guid("0fe424e1-769e-4443-b9c0-7529461fb02b"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9659), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 84, 69.44m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9659) },
                    { new Guid("13152d29-82a9-4696-bfee-b5de82396696"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(92), "Quisque vestibulum neque nec efficitur tincidunt.", 45, 6.13m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(93) },
                    { new Guid("166ec928-8fa5-4a65-8e94-ecf223086697"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9562), "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.", 72, 7.44m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9563) },
                    { new Guid("1aeb217f-7be8-407d-9029-624d1135dbde"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9431), "Integer ut velit sit amet velit luctus varius.", 66, 37.49m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9432) },
                    { new Guid("1b3afb4b-5a43-4ecd-a735-8dc66bfc5105"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9217), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 97, 83.69m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9217) },
                    { new Guid("207caeac-b110-4db6-a839-f0217b7542c0"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(377), "Sed ac odio eu orci luctus iaculis.", 41, 78.34m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(378) },
                    { new Guid("23c38bf9-2bf7-47dd-86af-bfd78aae4c04"), new Guid("9b2de812-fe5e-48b1-97c0-6383ece459e9"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9530), "Integer ut velit sit amet velit luctus varius.", 93, 90.74m, "Backpack", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9531) },
                    { new Guid("27b8086b-52be-41c6-935e-1ea50be95e45"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9171), "Proin vel tortor vel augue accumsan interdum id in justo.", 21, 14.61m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9172) },
                    { new Guid("27d6117c-bb11-474e-bebf-8645d8a4c1dc"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9359), "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.", 30, 36.47m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9360) },
                    { new Guid("2920eb37-02a9-428c-b975-74fb6959a42d"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9446), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 41, 86.27m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9447) },
                    { new Guid("2b40f1b5-316a-4474-a59a-64506ce0d3f4"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9116), "Integer ut velit sit amet velit luctus varius.", 15, 42.56m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9117) },
                    { new Guid("32ac6cc2-d769-40ac-96ab-ddb1c0a16606"), new Guid("5fadfac4-7607-4741-b3f9-c0519089b93e"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(564), "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.", 69, 52.64m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(565) },
                    { new Guid("32fc81e8-88f6-4f63-9210-c34a7cf9d033"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(645), "Proin vel tortor vel augue accumsan interdum id in justo.", 57, 94.33m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(646) },
                    { new Guid("39b7db3c-4dce-46b9-ac78-acfd9db548ce"), new Guid("40d8506b-d6de-4e44-853c-681e496c3afe"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(193), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 80, 10.70m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(194) },
                    { new Guid("3ae045c8-32c9-4017-b682-aa632733d979"), new Guid("9b2de812-fe5e-48b1-97c0-6383ece459e9"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9393), "Fusce eu libero eget arcu fermentum hendrerit id ut elit.", 38, 62.91m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9393) },
                    { new Guid("3be21dbd-15f8-45c2-a992-dea1f0eade37"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9289), "Integer ut velit sit amet velit luctus varius.", 16, 78.92m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9289) },
                    { new Guid("3c3df73e-3003-4737-8bbf-ee55426fe584"), new Guid("9b2de812-fe5e-48b1-97c0-6383ece459e9"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(703), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 29, 29.75m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(704) },
                    { new Guid("3c644029-46e8-424d-b82c-fa4238be9b4e"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(20), "Integer ut velit sit amet velit luctus varius.", 51, 3.99m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(21) },
                    { new Guid("3df5365e-896b-48ee-a6ba-cfdf82e52a0e"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9231), "Fusce eu libero eget arcu fermentum hendrerit id ut elit.", 48, 31.13m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9232) },
                    { new Guid("4016601a-e208-4589-9491-2858c5f10807"), new Guid("3f518f2f-8979-4f00-a1a2-d9cb6758e5b0"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(121), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 14, 0.97m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(122) },
                    { new Guid("49070ba1-dc38-477a-9fa6-66eb70d8b596"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9102), "Sed ac odio eu orci luctus iaculis.", 19, 24.88m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9102) },
                    { new Guid("4b83126b-45be-4091-ad47-ac7dd35c7f0e"), new Guid("fdaf25e3-0edd-41fe-b300-156260f62d57"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(346), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 78, 80.84m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(347) },
                    { new Guid("5d64819b-6e14-4300-9f1d-70234f54232c"), new Guid("85edc972-1610-44ac-b3e2-dcda859224e8"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(136), "Fusce eu libero eget arcu fermentum hendrerit id ut elit.", 82, 19.68m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(137) },
                    { new Guid("5e9fe89a-37f9-45b5-b244-2540d9d14b0d"), new Guid("5fadfac4-7607-4741-b3f9-c0519089b93e"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(222), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 16, 17.15m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(222) },
                    { new Guid("615c4468-44ec-4a9d-a9b5-978864e37e37"), new Guid("5fadfac4-7607-4741-b3f9-c0519089b93e"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(478), "Sed ac odio eu orci luctus iaculis.", 87, 44.67m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(478) },
                    { new Guid("6b57b4be-db69-4797-8d1f-cd578c37b9e9"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9675), "Sed ac odio eu orci luctus iaculis.", 6, 32.86m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9675) },
                    { new Guid("72956fd2-c1b6-4a3d-bca2-64bc76ba2f3f"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9187), "Sed ac odio eu orci luctus iaculis.", 96, 98.50m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9188) },
                    { new Guid("78873241-9281-494e-80d2-8d28d0206e2d"), new Guid("85edc972-1610-44ac-b3e2-dcda859224e8"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(506), "Proin vel tortor vel augue accumsan interdum id in justo.", 5, 25.71m, "Smartphone", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(507) },
                    { new Guid("847435d7-d8b5-4501-9a16-f3a32786f659"), new Guid("40d8506b-d6de-4e44-853c-681e496c3afe"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(448), "Integer ut velit sit amet velit luctus varius.", 10, 23.58m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(449) },
                    { new Guid("8ec5f366-67e0-4311-a1bb-2bc6218749df"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9461), "Etiam ullamcorper odio eu libero varius, eu feugiat dolor iaculis.", 93, 12.24m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9462) },
                    { new Guid("90302e91-c81f-41d8-bd5d-ac516f4139e5"), new Guid("85edc972-1610-44ac-b3e2-dcda859224e8"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(207), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 52, 18.59m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(208) },
                    { new Guid("94050bfc-62c9-4516-89da-b83be428b3c2"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9068), "Sed ac odio eu orci luctus iaculis.", 98, 54.91m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9069) },
                    { new Guid("95385d0c-51a6-42da-a4d6-e5fcaf7930e8"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(76), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 99, 78.65m, "Digital Camera", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(77) },
                    { new Guid("9deb37cf-bf7a-4a70-8a8f-45513d827868"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 414, DateTimeKind.Local).AddTicks(8686), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 77, 15100m, "Laptop", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(7800) },
                    { new Guid("a4f1760b-48ac-49ee-bb5b-64c98783e6e8"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9866), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 52, 57.40m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9867) },
                    { new Guid("a9abd9a0-c6e6-4ba8-9edd-ea9b9675c550"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9809), "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.", 41, 33.51m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9810) },
                    { new Guid("aa23694c-00f7-4185-9702-c8fd93526a70"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9272), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 41, 16.91m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9272) },
                    { new Guid("af018b8a-5678-48a5-bbd8-5fe97c828343"), new Guid("40d8506b-d6de-4e44-853c-681e496c3afe"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(578), "Etiam ullamcorper odio eu libero varius, eu feugiat dolor iaculis.", 27, 91.74m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(579) },
                    { new Guid("b3bece04-9ac4-4410-8270-342b3b5cff0f"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(675), "Quisque vestibulum neque nec efficitur tincidunt.", 97, 40.15m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(675) },
                    { new Guid("bb0b6b1a-bbdc-4d25-b47f-a6989ff015a4"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9475), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 34, 55.05m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9476) },
                    { new Guid("c3e6dedb-928e-4611-aad4-ca312ebe80ef"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9202), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 87, 10.61m, "Backpack", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9203) },
                    { new Guid("cd287de9-163b-4433-b40e-06bbb6b8b796"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9779), "Integer ut velit sit amet velit luctus varius.", 7, 45.81m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9780) },
                    { new Guid("ce6d9918-3ef2-401d-a818-2db1b17c60ee"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(362), "Proin vel tortor vel augue accumsan interdum id in justo.", 68, 36.77m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(363) },
                    { new Guid("d1810ab0-11d6-40ec-9378-234b0cb9bd83"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9378), "Sed ac odio eu orci luctus iaculis.", 53, 74.64m, "Gaming Console", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9379) },
                    { new Guid("d32b5c89-6776-420d-b5e3-31612cd5061c"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9704), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 71, 36.42m, "Coffee Maker", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9705) },
                    { new Guid("d504c2b1-5419-4f76-a01e-6461ab6169be"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(408), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 83, 39.28m, "Backpack", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(409) },
                    { new Guid("d72edce2-0e6e-432d-aba8-f3bdf4a3ada1"), new Guid("3f518f2f-8979-4f00-a1a2-d9cb6758e5b0"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(236), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 83, 91.95m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(237) },
                    { new Guid("dcdbfd32-c8a2-449b-aa5d-e09da0a2cd63"), new Guid("9b2de812-fe5e-48b1-97c0-6383ece459e9"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(718), "Vivamus volutpat odio nec enim volutpat, ac sodales velit fermentum.", 59, 70.69m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(719) },
                    { new Guid("dd8e6825-4e35-45ff-8847-a8223344f676"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9086), "Integer ut velit sit amet velit luctus varius.", 27, 35.18m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9087) },
                    { new Guid("e2c8bb63-0748-425c-80d6-5aa6828e2d4f"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(4), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 12, 6.06m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(5) },
                    { new Guid("e5df9404-3885-431e-a168-fecf6e03006d"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9029), "Quisque vestibulum neque nec efficitur tincidunt.", 81, 33.32m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9033) },
                    { new Guid("e9fc2e2c-5afe-4f66-9de9-0b7e1f9049be"), new Guid("7ab19f86-c418-4705-b0cc-72a8f012b5c6"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9794), "Aliquam auctor justo a tellus bibendum, vitae accumsan purus bibendum.", 25, 96.39m, "Headphones", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9795) },
                    { new Guid("ee87f96a-66dd-48fd-a65c-16a3cffd96db"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(546), "Integer ut velit sit amet velit luctus varius.", 73, 73.98m, "TV", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(547) },
                    { new Guid("efaf5581-bab3-49f6-9ce9-4e799b9c7bce"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(492), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 78, 36.36m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(493) },
                    { new Guid("f27aba4e-0dd0-49f7-a8a4-c12ceb3e43c9"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9722), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 51, 69.42m, "Backpack", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9723) },
                    { new Guid("f41e0c33-e149-436f-82fd-f6847aad0274"), new Guid("15470236-9a28-4669-9547-a80ea388a5cc"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9764), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 58, 93.74m, "Digital Camera", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9764) },
                    { new Guid("f5eaab0f-f299-4de6-9d60-1728b69b3079"), new Guid("6cd7df58-7a5d-43c0-8c60-2a1384a4289b"), new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9881), "Praesent dapibus justo ut felis dignissim, a tristique velit varius.", 23, 63.49m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 415, DateTimeKind.Local).AddTicks(9882) },
                    { new Guid("f7b7bb73-e690-4677-bde4-b7c521df2787"), new Guid("5fadfac4-7607-4741-b3f9-c0519089b93e"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(689), "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", 88, 56.51m, "Digital Camera", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(690) },
                    { new Guid("fda86b05-0079-4256-8cdd-65197656b89c"), new Guid("627d40cb-a2e7-4355-b48b-a667212ca6d6"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(35), "Etiam ullamcorper odio eu libero varius, eu feugiat dolor iaculis.", 17, 80.76m, "Bookshelf", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(36) },
                    { new Guid("ff9e48ab-9b83-46bc-a8c3-22ba141c79e0"), new Guid("40d8506b-d6de-4e44-853c-681e496c3afe"), new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(178), "Sed ac odio eu orci luctus iaculis.", 64, 65.17m, "Running Shoes", new DateTime(2023, 12, 25, 0, 28, 41, 416, DateTimeKind.Local).AddTicks(179) }
                });

            migrationBuilder.InsertData(
                table: "images",
                columns: new[] { "id", "created_at", "image_url", "product_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("01b6a6f1-8d3f-4934-94ec-f4925a6a194c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7149), "https://i.imgur.com/WwKucXb.jpeg", new Guid("d32b5c89-6776-420d-b5e3-31612cd5061c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7150) },
                    { new Guid("02871dca-aa79-41bb-8449-360fbec3c401"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7879), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("847435d7-d8b5-4501-9a16-f3a32786f659"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7880) },
                    { new Guid("0350d656-6bb6-4e62-866c-df4fce9a9b64"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7262), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("091347ce-107e-444d-86ff-83e914824bbc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7262) },
                    { new Guid("049a5e38-d18b-48e9-82e8-4dca2f5ca046"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7399), "https://i.imgur.com/mWwek7p.jpeg", new Guid("3c644029-46e8-424d-b82c-fa4238be9b4e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7400) },
                    { new Guid("04c7f12a-60ee-46cf-a908-6af611a5ee45"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7720), "https://i.imgur.com/J6MinJn.jpeg", new Guid("4b83126b-45be-4091-ad47-ac7dd35c7f0e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7721) },
                    { new Guid("0758615f-3217-4490-9b01-5c53ce55bc87"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7531), "https://placeimg.com/640/480/any", new Guid("5d64819b-6e14-4300-9f1d-70234f54232c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7532) },
                    { new Guid("07c3f58a-fd46-4211-8de4-82df2a192ad9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6944), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("0493cf0a-d7b0-405b-8146-f9233c19ee26"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6944) },
                    { new Guid("0b087094-8419-4b41-b272-905102e5ef9c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7133), "https://i.imgur.com/WwKucXb.jpeg", new Guid("02904361-4464-481f-a033-d26330da5a2e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7133) },
                    { new Guid("0c68ad30-67d1-47cc-bd54-c0050f5555fd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8140), "https://i.imgur.com/mWwek7p.jpeg", new Guid("f7b7bb73-e690-4677-bde4-b7c521df2787"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8141) },
                    { new Guid("1083a1e7-b530-4a17-98c7-532abad9751b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8032), "https://i.imgur.com/J6MinJn.jpeg", new Guid("32fc81e8-88f6-4f63-9210-c34a7cf9d033"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8033) },
                    { new Guid("11ad9b35-b940-441a-add4-33cd6b4ff7b0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7703), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("4b83126b-45be-4091-ad47-ac7dd35c7f0e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7704) },
                    { new Guid("122f44b5-3a17-4552-8bf2-7a46ea1e0699"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6420), "https://i.imgur.com/mWwek7p.jpeg", new Guid("49070ba1-dc38-477a-9fa6-66eb70d8b596"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6421) },
                    { new Guid("137c2da1-e180-4a01-bb87-46784ee1aa3d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6457), "https://i.imgur.com/mWwek7p.jpeg", new Guid("27b8086b-52be-41c6-935e-1ea50be95e45"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6458) },
                    { new Guid("1393d68d-7e55-46cb-94c7-df7abb4347e2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6357), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("e5df9404-3885-431e-a168-fecf6e03006d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6358) },
                    { new Guid("163f30d3-0bed-4909-a82b-2e32f509cba1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7934), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("efaf5581-bab3-49f6-9ce9-4e799b9c7bce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7934) },
                    { new Guid("198e4f91-ead7-4b0e-915f-b8833d098974"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7447), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("95385d0c-51a6-42da-a4d6-e5fcaf7930e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7448) },
                    { new Guid("1b3ab19d-05a9-45b7-abc5-1d0b0fa4d7cc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7362), "https://placeimg.com/640/480/any", new Guid("f5eaab0f-f299-4de6-9d60-1728b69b3079"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7363) },
                    { new Guid("1bce6150-1d66-4147-90d8-f984238b68ce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6980), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("0bd1cdaf-f4bc-459a-a675-c0e86b5b3afb"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6980) },
                    { new Guid("1df16ac6-3e78-4e68-a826-36bc82e6ce88"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6369), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("94050bfc-62c9-4516-89da-b83be428b3c2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6370) },
                    { new Guid("200e9a22-7d47-4ca5-9ea7-886526abf7d3"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6426), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("49070ba1-dc38-477a-9fa6-66eb70d8b596"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6427) },
                    { new Guid("2027c8a8-63fb-4a35-80a5-6743c011439a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7653), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("90302e91-c81f-41d8-bd5d-ac516f4139e5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7653) },
                    { new Guid("20d163c4-835f-4a9a-a362-d3af39562c43"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7000), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("02beea56-a18f-4405-a2d7-7e183cc6953d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7000) },
                    { new Guid("22a0d490-3cfa-4599-9e34-91935708c44c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6330), "https://placeimg.com/640/480/any", new Guid("9deb37cf-bf7a-4a70-8a8f-45513d827868"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6331) },
                    { new Guid("232ddf23-a59e-42d5-beb3-5499d4d212d6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7875), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("847435d7-d8b5-4501-9a16-f3a32786f659"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7876) },
                    { new Guid("2549581f-f824-4698-a16b-f4519c427433"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7004), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("02beea56-a18f-4405-a2d7-7e183cc6953d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7005) },
                    { new Guid("25a0c269-41d4-4a00-b400-2707597dc546"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7798), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("d504c2b1-5419-4f76-a01e-6461ab6169be"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7801) },
                    { new Guid("266b9ffc-e61e-490c-9578-fec78abb2269"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7660), "https://i.imgur.com/WwKucXb.jpeg", new Guid("5e9fe89a-37f9-45b5-b244-2540d9d14b0d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7661) },
                    { new Guid("2699a015-c786-4964-b002-a8856d7a984a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8156), "https://i.imgur.com/WwKucXb.jpeg", new Guid("3c3df73e-3003-4737-8bbf-ee55426fe584"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8157) },
                    { new Guid("2824b03b-aa77-474e-bb3a-70e8028a9a85"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6897), "https://placeimg.com/640/480/any", new Guid("bb0b6b1a-bbdc-4d25-b47f-a6989ff015a4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6898) },
                    { new Guid("28a498d1-4843-465d-93b3-c4f160b9b484"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7297), "https://placeimg.com/640/480/any", new Guid("f5eaab0f-f299-4de6-9d60-1728b69b3079"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7297) },
                    { new Guid("2b618153-0711-497a-82a4-bde0a7c05876"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7011), "https://placeimg.com/640/480/any", new Guid("0fe424e1-769e-4443-b9c0-7529461fb02b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7012) },
                    { new Guid("2ba0ee97-6144-4fd5-a926-83d79f7413fc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6488), "https://i.imgur.com/WwKucXb.jpeg", new Guid("72956fd2-c1b6-4a3d-bca2-64bc76ba2f3f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6488) },
                    { new Guid("2bdf2bd0-2244-45a2-917f-0e3b19472da5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8175), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("dcdbfd32-c8a2-449b-aa5d-e09da0a2cd63"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8176) },
                    { new Guid("2dc66c57-7242-41c5-ad6b-52af9df05791"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6446), "https://placeimg.com/640/480/any", new Guid("2b40f1b5-316a-4474-a59a-64506ce0d3f4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6447) },
                    { new Guid("2efc7545-5434-4347-aa37-6c5fa2d7fe7d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6904), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("047f5e4c-3274-4e74-8ba5-cdebea954b9e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6904) },
                    { new Guid("303ea2a7-527f-4e9a-bf04-502400d65a46"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7620), "https://i.imgur.com/J6MinJn.jpeg", new Guid("ff9e48ab-9b83-46bc-a8c3-22ba141c79e0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7621) },
                    { new Guid("31b9d13b-c286-4bdc-b67b-955a68d05786"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6494), "https://i.imgur.com/J6MinJn.jpeg", new Guid("72956fd2-c1b6-4a3d-bca2-64bc76ba2f3f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6495) },
                    { new Guid("3221c3d3-bee4-454a-b8fb-483ba28b6616"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6342), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("e5df9404-3885-431e-a168-fecf6e03006d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6343) },
                    { new Guid("33796af6-5e5d-4508-a94f-dbbc1634f079"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7899), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("00a2d36b-38f9-4c89-8896-6ca91a7cde8d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7900) },
                    { new Guid("3527bd33-4a85-4f35-8809-aeda5b9cee4d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6466), "https://placeimg.com/640/480/any", new Guid("27b8086b-52be-41c6-935e-1ea50be95e45"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6467) },
                    { new Guid("359ef6f4-d986-4fac-8295-cb703ffb0512"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6925), "https://placeimg.com/640/480/any", new Guid("23c38bf9-2bf7-47dd-86af-bfd78aae4c04"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6926) },
                    { new Guid("3680ea49-7143-4926-8fa9-ca6524c484f8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7625), "https://placeimg.com/640/480/any", new Guid("39b7db3c-4dce-46b9-ac78-acfd9db548ce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7626) },
                    { new Guid("36af711c-df2d-4bdd-a287-b0c0997f629f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6671), "https://placeimg.com/640/480/any", new Guid("aa23694c-00f7-4185-9702-c8fd93526a70"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6672) },
                    { new Guid("38a3c293-651d-460f-ba51-8a8bd04790a4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6453), "https://i.imgur.com/mWwek7p.jpeg", new Guid("2b40f1b5-316a-4474-a59a-64506ce0d3f4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6454) },
                    { new Guid("3b0aa42c-0bc8-4882-a5a0-b4d15c932aa0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7727), "https://placeimg.com/640/480/any", new Guid("ce6d9918-3ef2-401d-a818-2db1b17c60ee"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7728) },
                    { new Guid("3ecc9d00-6f65-4b57-b8e6-9d416a199f4d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8039), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("32fc81e8-88f6-4f63-9210-c34a7cf9d033"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8040) },
                    { new Guid("47392a9b-4190-441a-8448-7206d47b2f4c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7911), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("615c4468-44ec-4a9d-a9b5-978864e37e37"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7911) },
                    { new Guid("473fd52d-b373-4be1-bc44-139771f71059"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7210), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("cd287de9-163b-4433-b40e-06bbb6b8b796"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7211) },
                    { new Guid("4792870a-7437-473b-b1f1-f3f6ab8ca8f4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6395), "https://placeimg.com/640/480/any", new Guid("dd8e6825-4e35-45ff-8847-a8223344f676"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6396) },
                    { new Guid("47a259f6-a9bc-4853-8b2e-32ca788acee7"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7471), "https://i.imgur.com/mWwek7p.jpeg", new Guid("13152d29-82a9-4696-bfee-b5de82396696"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7472) },
                    { new Guid("47af83d6-205d-4b23-92f4-986f27dd7f53"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6909), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("047f5e4c-3274-4e74-8ba5-cdebea954b9e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6912) },
                    { new Guid("47c0a6d0-f556-4d6a-aeac-7ed23c481b23"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7417), "https://placeimg.com/640/480/any", new Guid("fda86b05-0079-4256-8cdd-65197656b89c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7418) },
                    { new Guid("49c17194-8196-445d-8c96-2a77fb0efad5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8164), "https://placeimg.com/640/480/any", new Guid("3c3df73e-3003-4737-8bbf-ee55426fe584"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8164) },
                    { new Guid("4a963170-1c0d-4cc4-9739-cfd12c62a358"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6434), "https://i.imgur.com/WwKucXb.jpeg", new Guid("49070ba1-dc38-477a-9fa6-66eb70d8b596"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6435) },
                    { new Guid("4b146a28-cdd5-4b83-a12b-d6f7078b9a7b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7510), "https://i.imgur.com/mWwek7p.jpeg", new Guid("0cdb8bd1-cf78-4d3c-94aa-ca8afe4b4319"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7511) },
                    { new Guid("4bae4e10-4a72-4c43-8f9a-768afe0085cc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7752), "https://i.imgur.com/J6MinJn.jpeg", new Guid("207caeac-b110-4db6-a839-f0217b7542c0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7752) },
                    { new Guid("4c95cf64-894f-405f-b260-d5ade2cd86d5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7219), "https://i.imgur.com/mWwek7p.jpeg", new Guid("cd287de9-163b-4433-b40e-06bbb6b8b796"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7219) },
                    { new Guid("4d2f3eaa-7079-48dc-9c86-b2c1a10d738a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6988), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("0bd1cdaf-f4bc-459a-a675-c0e86b5b3afb"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6989) },
                    { new Guid("4db4b47b-82a6-407b-860e-86fdd67e15d6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6782), "https://placeimg.com/640/480/any", new Guid("2920eb37-02a9-428c-b975-74fb6959a42d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6783) },
                    { new Guid("4df3a7f1-b79e-4643-9ab7-b0f1af35f1f1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7027), "https://placeimg.com/640/480/any", new Guid("6b57b4be-db69-4797-8d1f-cd578c37b9e9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7028) },
                    { new Guid("4e7e84f3-0f11-47be-86a5-acb02b733c5c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6411), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("dd8e6825-4e35-45ff-8847-a8223344f676"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6412) },
                    { new Guid("51a83dc7-6ed2-4e4a-ab58-6f79df4a507f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8179), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("dcdbfd32-c8a2-449b-aa5d-e09da0a2cd63"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8180) },
                    { new Guid("53e847a7-53d9-4722-b540-b7422e4b8689"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7269), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("091347ce-107e-444d-86ff-83e914824bbc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7270) },
                    { new Guid("548ffbe6-8aad-4ba2-a1f5-e6345094b512"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8107), "https://placeimg.com/640/480/any", new Guid("00e4725f-3c80-4efd-84bc-d42586661139"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8107) },
                    { new Guid("560b60d0-f678-4a52-973d-8137a72b91e7"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6742), "https://i.imgur.com/WwKucXb.jpeg", new Guid("3ae045c8-32c9-4017-b682-aa632733d979"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6743) },
                    { new Guid("57a4d39f-413d-4f48-ab8f-9846b17acab4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7109), "https://i.imgur.com/J6MinJn.jpeg", new Guid("6b57b4be-db69-4797-8d1f-cd578c37b9e9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7110) },
                    { new Guid("583ec697-ddf4-48d0-b175-622ad5f4cb3c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7242), "https://placeimg.com/640/480/any", new Guid("a9abd9a0-c6e6-4ba8-9edd-ea9b9675c550"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7242) },
                    { new Guid("5af98760-9618-4b45-877d-453c34148ad8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6920), "https://i.imgur.com/WwKucXb.jpeg", new Guid("047f5e4c-3274-4e74-8ba5-cdebea954b9e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6921) },
                    { new Guid("5ff5e0fc-4d3b-4abb-880f-79c5ce6408a1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7891), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("00a2d36b-38f9-4c89-8896-6ca91a7cde8d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7891) },
                    { new Guid("61d851f4-e9ce-4080-b2b9-1cf316c40e78"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6879), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("bb0b6b1a-bbdc-4d25-b47f-a6989ff015a4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6880) },
                    { new Guid("62577a67-20b5-4376-90a7-956fd25dce2f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7180), "https://placeimg.com/640/480/any", new Guid("f27aba4e-0dd0-49f7-a8a4-c12ceb3e43c9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7181) },
                    { new Guid("62fca461-babe-4764-a134-142c6fa2c16e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8120), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("b3bece04-9ac4-4410-8270-342b3b5cff0f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8121) },
                    { new Guid("6527eb14-52cc-4724-a2ee-55f289fb291f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7918), "https://placeimg.com/640/480/any", new Guid("615c4468-44ec-4a9d-a9b5-978864e37e37"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7918) },
                    { new Guid("6543c2d5-c8cb-4cec-92a3-31d6c0b2310a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7388), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("3c644029-46e8-424d-b82c-fa4238be9b4e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7389) },
                    { new Guid("65797a49-e9bd-4c77-8f1e-dfa3c590f03a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7246), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("a9abd9a0-c6e6-4ba8-9edd-ea9b9675c550"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7247) },
                    { new Guid("65ce3a9f-bffc-4fb3-8990-a90dde03a17d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6984), "https://placeimg.com/640/480/any", new Guid("0bd1cdaf-f4bc-459a-a675-c0e86b5b3afb"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6984) },
                    { new Guid("661fa4b9-6528-4bb5-9219-5dce88fef1a8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6528), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("1b3afb4b-5a43-4ecd-a735-8dc66bfc5105"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6529) },
                    { new Guid("669c826c-175e-4bd4-8ff7-dabe8a4cb285"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7609), "https://i.imgur.com/WwKucXb.jpeg", new Guid("ff9e48ab-9b83-46bc-a8c3-22ba141c79e0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7610) },
                    { new Guid("6776b3af-7313-41bb-877e-e3e48ad42bac"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6749), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("1aeb217f-7be8-407d-9029-624d1135dbde"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6750) },
                    { new Guid("67f38acd-9424-48e6-acb1-747e5a4fb53e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6534), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("1b3afb4b-5a43-4ecd-a735-8dc66bfc5105"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6535) },
                    { new Guid("67f52fde-947f-4fd3-84cf-2878f01dcca0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7440), "https://placeimg.com/640/480/any", new Guid("95385d0c-51a6-42da-a4d6-e5fcaf7930e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7441) },
                    { new Guid("691f02aa-9f01-4738-b4ab-b08dad75946e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6699), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("27d6117c-bb11-474e-bebf-8645d8a4c1dc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6699) },
                    { new Guid("698c75fe-cc19-4485-93d0-91db21f967b7"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7258), "https://i.imgur.com/mWwek7p.jpeg", new Guid("091347ce-107e-444d-86ff-83e914824bbc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7258) },
                    { new Guid("6c5c376a-adfd-4809-ba58-d7257f23006c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7926), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("efaf5581-bab3-49f6-9ce9-4e799b9c7bce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7927) },
                    { new Guid("6cb1c174-40a8-4287-9e21-203312e7a59e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7599), "https://placeimg.com/640/480/any", new Guid("5d64819b-6e14-4300-9f1d-70234f54232c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7600) },
                    { new Guid("6d396e5f-33ea-4a5a-be19-5f2fc259cd69"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7768), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("207caeac-b110-4db6-a839-f0217b7542c0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7769) },
                    { new Guid("6e3cfb96-f81d-4e4e-a323-b975882ffa4e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6511), "https://i.imgur.com/WwKucXb.jpeg", new Guid("c3e6dedb-928e-4611-aad4-ca312ebe80ef"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6512) },
                    { new Guid("70108298-ac9d-41c2-a571-5b13c704d046"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7198), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("f41e0c33-e149-436f-82fd-f6847aad0274"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7199) },
                    { new Guid("713af960-558a-44ff-ba01-3cbd1f1c3ffe"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6653), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("3df5365e-896b-48ee-a6ba-cfdf82e52a0e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6653) },
                    { new Guid("713f9d61-4d4e-4176-9946-550c07c254a6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6968), "https://placeimg.com/640/480/any", new Guid("166ec928-8fa5-4a65-8e94-ecf223086697"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6969) },
                    { new Guid("71415ee5-0481-45f5-9bbb-b57802389384"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7992), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("32ac6cc2-d769-40ac-96ab-ddb1c0a16606"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7993) },
                    { new Guid("7256745b-e783-42e4-9b7a-e71233bf738b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7122), "https://i.imgur.com/mWwek7p.jpeg", new Guid("02904361-4464-481f-a033-d26330da5a2e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7123) },
                    { new Guid("72d10290-8814-49ba-ac68-7a1a4e9ff44f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7792), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("05d0c36a-0fd0-4d81-83d7-67b5d96316fc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7793) },
                    { new Guid("72f647ba-e36f-4014-a221-0ac6b7a18dae"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7637), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("39b7db3c-4dce-46b9-ac78-acfd9db548ce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7637) },
                    { new Guid("72fc6017-5a61-4450-a062-7f91eb5d121b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7453), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("95385d0c-51a6-42da-a4d6-e5fcaf7930e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7454) },
                    { new Guid("7306251b-84bd-4899-8959-e2ed259d2317"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6667), "https://placeimg.com/640/480/any", new Guid("aa23694c-00f7-4185-9702-c8fd93526a70"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6667) },
                    { new Guid("753bb59c-b9c0-487f-9161-b0f7f37d993b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6715), "https://placeimg.com/640/480/any", new Guid("d1810ab0-11d6-40ec-9378-234b0cb9bd83"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6715) },
                    { new Guid("77b10157-9d24-4c9b-a280-0af1d947c823"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6754), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("1aeb217f-7be8-407d-9029-624d1135dbde"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6754) },
                    { new Guid("77eb0a15-9461-4029-89f6-b4f02c6c1b33"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7285), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("a4f1760b-48ac-49ee-bb5b-64c98783e6e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7286) },
                    { new Guid("7830f886-ab81-46dc-8869-cff93fcf9c41"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7493), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("0cdb8bd1-cf78-4d3c-94aa-ca8afe4b4319"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7494) },
                    { new Guid("79282e9d-0606-44ba-a4f1-f74fc9b65043"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6706), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("27d6117c-bb11-474e-bebf-8645d8a4c1dc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6707) },
                    { new Guid("79a6ddde-e714-499f-94e2-6dd9b2dc2d1b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7863), "https://i.imgur.com/WwKucXb.jpeg", new Guid("d504c2b1-5419-4f76-a01e-6461ab6169be"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7864) },
                    { new Guid("79ca5458-587b-4c80-956d-899e5af20461"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6957), "https://i.imgur.com/WwKucXb.jpeg", new Guid("0493cf0a-d7b0-405b-8146-f9233c19ee26"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6957) },
                    { new Guid("7a7f2105-4e9b-43b4-b389-fbaf60681280"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6769), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("2920eb37-02a9-428c-b975-74fb6959a42d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6770) },
                    { new Guid("7bd6410e-669f-4f8d-8b4e-05c775e93c5a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6517), "https://placeimg.com/640/480/any", new Guid("c3e6dedb-928e-4611-aad4-ca312ebe80ef"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6518) },
                    { new Guid("7d4d0073-7520-4743-ad1b-5e62c6947ff4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7678), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("d72edce2-0e6e-432d-aba8-f3bdf4a3ada1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7680) },
                    { new Guid("7ed88ead-6e1a-4528-9012-2aee4c1cebd3"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6953), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("0493cf0a-d7b0-405b-8146-f9233c19ee26"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6953) },
                    { new Guid("7f0c58a3-4a5f-41bf-a29d-6d141c2bac82"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6472), "https://i.imgur.com/WwKucXb.jpeg", new Guid("27b8086b-52be-41c6-935e-1ea50be95e45"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6473) },
                    { new Guid("7f132f77-4e99-49e4-b36c-939a8f1cacc1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7032), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("6b57b4be-db69-4797-8d1f-cd578c37b9e9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7032) },
                    { new Guid("8124d565-a2f0-4e44-9d94-247994713381"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7786), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("05d0c36a-0fd0-4d81-83d7-67b5d96316fc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7787) },
                    { new Guid("83c95195-3ce0-449d-ad43-39c6f82c433d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6375), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("94050bfc-62c9-4516-89da-b83be428b3c2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6376) },
                    { new Guid("86087fcd-1e36-4ae1-a79b-0a088788cf2b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6349), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("e5df9404-3885-431e-a168-fecf6e03006d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6350) },
                    { new Guid("86d971e3-e15f-4056-8371-2619d3b5a0de"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6800), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("8ec5f366-67e0-4311-a1bb-2bc6218749df"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6801) },
                    { new Guid("894d6811-6f52-4e88-ad6b-fef1c932e023"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7203), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("f41e0c33-e149-436f-82fd-f6847aad0274"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7203) },
                    { new Guid("8b4ad749-85a2-43ac-a04a-68920a460368"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6478), "https://placeimg.com/640/480/any", new Guid("72956fd2-c1b6-4a3d-bca2-64bc76ba2f3f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6479) },
                    { new Guid("8dd26d30-c84e-4c2a-9934-8ccc81552991"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6687), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("3be21dbd-15f8-45c2-a992-dea1f0eade37"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6688) },
                    { new Guid("8e388f05-c445-44c5-a8ed-1278f5b77019"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6738), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("3ae045c8-32c9-4017-b682-aa632733d979"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6738) },
                    { new Guid("919a50e8-3601-4437-a10e-4814777b998f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7998), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("af018b8a-5678-48a5-bbd8-5fe97c828343"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7999) },
                    { new Guid("93dbe527-4416-4b7a-ad25-7a67886931f7"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7519), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("4016601a-e208-4589-9491-2858c5f10807"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7520) },
                    { new Guid("943deda4-851f-4dc5-8e6b-23d8f54c9dd2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7016), "https://placeimg.com/640/480/any", new Guid("0fe424e1-769e-4443-b9c0-7529461fb02b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7016) },
                    { new Guid("94a0abb6-973c-40f6-82f0-2a73974a56b7"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7633), "https://placeimg.com/640/480/any", new Guid("39b7db3c-4dce-46b9-ac78-acfd9db548ce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7633) },
                    { new Guid("9585dc65-d905-434d-a142-df949f2520b8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8051), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("00e4725f-3c80-4efd-84bc-d42586661139"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8052) },
                    { new Guid("98f8cbf4-2cfc-45a8-b30e-0e092611f268"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6759), "https://placeimg.com/640/480/any", new Guid("1aeb217f-7be8-407d-9029-624d1135dbde"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6760) },
                    { new Guid("9947bfbf-b398-423c-9202-d4bb3737c68e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7922), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("efaf5581-bab3-49f6-9ce9-4e799b9c7bce"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7923) },
                    { new Guid("994d0b1c-1e8c-4b8f-97ab-ed8cc3a4fe5a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6660), "https://i.imgur.com/WwKucXb.jpeg", new Guid("3df5365e-896b-48ee-a6ba-cfdf82e52a0e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6661) },
                    { new Guid("9a1c7acb-43e0-4d9c-8e3d-74df8d829a1f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7710), "https://i.imgur.com/mWwek7p.jpeg", new Guid("4b83126b-45be-4091-ad47-ac7dd35c7f0e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7710) },
                    { new Guid("9b0a2c9d-2fff-4c80-aa5b-4125df339238"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6939), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("23c38bf9-2bf7-47dd-86af-bfd78aae4c04"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6940) },
                    { new Guid("9b4659f2-1f50-4265-bfba-c7efb546e66f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8152), "https://placeimg.com/640/480/any", new Guid("3c3df73e-3003-4737-8bbf-ee55426fe584"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8153) },
                    { new Guid("9c6b0fa0-4968-44bc-9733-80a9f4bb4fef"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6961), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("166ec928-8fa5-4a65-8e94-ecf223086697"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6962) },
                    { new Guid("9d57c5ba-36e3-4efc-91d6-4dc14ba8fa5f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8124), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("b3bece04-9ac4-4410-8270-342b3b5cff0f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8125) },
                    { new Guid("9f6ed72a-0eff-403b-9b2a-ebc337b2ad2b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7234), "https://i.imgur.com/mWwek7p.jpeg", new Guid("e9fc2e2c-5afe-4f66-9de9-0b7e1f9049be"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7235) },
                    { new Guid("a075e2cd-85b6-4bf7-963c-48626cc14392"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7253), "https://placeimg.com/640/480/any", new Guid("a9abd9a0-c6e6-4ba8-9edd-ea9b9675c550"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7254) },
                    { new Guid("a29baecd-4827-4b0d-8163-72c92c405d0b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7961), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("ee87f96a-66dd-48fd-a65c-16a3cffd96db"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7962) },
                    { new Guid("a2d6b844-1c4e-468e-8a1e-af130fb9a38e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7942), "https://placeimg.com/640/480/any", new Guid("78873241-9281-494e-80d2-8d28d0206e2d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7943) },
                    { new Guid("a4816401-6c48-43c8-9781-0dda98d30bca"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6886), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("bb0b6b1a-bbdc-4d25-b47f-a6989ff015a4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6887) },
                    { new Guid("a53008ac-7703-47de-ae11-dcd094b74533"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7668), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("5e9fe89a-37f9-45b5-b244-2540d9d14b0d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7669) },
                    { new Guid("a59ec859-96ed-4c5e-b51f-48213eeda741"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8136), "https://i.imgur.com/J6MinJn.jpeg", new Guid("f7b7bb73-e690-4677-bde4-b7c521df2787"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8137) },
                    { new Guid("a6a7b07c-cab3-401d-9868-fffb9231f53e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7745), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("ce6d9918-3ef2-401d-a818-2db1b17c60ee"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7746) },
                    { new Guid("a6c6160d-cbac-4f24-bd77-321b71ad8ccd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7290), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("f5eaab0f-f299-4de6-9d60-1728b69b3079"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7290) },
                    { new Guid("a775b30a-e496-4b73-b846-a591a0173561"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6385), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("94050bfc-62c9-4516-89da-b83be428b3c2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6386) },
                    { new Guid("a897b9dc-5559-4ce4-8040-36f3f7a5bad3"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6648), "https://placeimg.com/640/480/any", new Guid("3df5365e-896b-48ee-a6ba-cfdf82e52a0e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6649) },
                    { new Guid("a8af2817-f8cf-4195-a78c-92e92a448f1c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6726), "https://i.imgur.com/mWwek7p.jpeg", new Guid("d1810ab0-11d6-40ec-9378-234b0cb9bd83"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6726) },
                    { new Guid("a9f9829e-ad56-47bf-9076-a4178e4dfc2c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6683), "https://i.imgur.com/J6MinJn.jpeg", new Guid("3be21dbd-15f8-45c2-a992-dea1f0eade37"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6684) },
                    { new Guid("aa8ecb51-d98e-4ef6-87ac-3bfee7b5715f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6439), "https://i.imgur.com/J6MinJn.jpeg", new Guid("2b40f1b5-316a-4474-a59a-64506ce0d3f4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6440) },
                    { new Guid("acbf7749-8523-4a3f-9dfa-861435e97326"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7949), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("78873241-9281-494e-80d2-8d28d0206e2d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7950) },
                    { new Guid("ad5889af-aa06-4256-86f3-d459b763066a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6694), "https://placeimg.com/640/480/any", new Guid("3be21dbd-15f8-45c2-a992-dea1f0eade37"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6695) },
                    { new Guid("b0e37948-3ede-4868-a315-f8b44fb06af3"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7648), "https://i.imgur.com/WwKucXb.jpeg", new Guid("90302e91-c81f-41d8-bd5d-ac516f4139e5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7649) },
                    { new Guid("b43687ec-26f4-4f00-9b55-c4af697faf7f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6996), "https://i.imgur.com/J6MinJn.jpeg", new Guid("02beea56-a18f-4405-a2d7-7e183cc6953d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6996) },
                    { new Guid("b50d8f88-d505-4704-bc73-6af044c7f5c5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7424), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("fda86b05-0079-4256-8cdd-65197656b89c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7424) },
                    { new Guid("b51dcc79-f0d2-4a59-b62d-57db356426bc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7664), "https://i.imgur.com/mWwek7p.jpeg", new Guid("5e9fe89a-37f9-45b5-b244-2540d9d14b0d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7665) },
                    { new Guid("b545c948-1b08-4897-a181-6790442d44a3"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8131), "https://i.imgur.com/WwKucXb.jpeg", new Guid("b3bece04-9ac4-4410-8270-342b3b5cff0f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8132) },
                    { new Guid("b8cbf513-2480-4c08-b481-0bc95d805e27"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8168), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("dcdbfd32-c8a2-449b-aa5d-e09da0a2cd63"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8169) },
                    { new Guid("b9117468-2536-4608-9879-158bc7929d3f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7954), "https://placeimg.com/640/480/any", new Guid("ee87f96a-66dd-48fd-a65c-16a3cffd96db"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7954) },
                    { new Guid("ba152802-8ea3-45a0-a5dd-690438e1aea6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7686), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("d72edce2-0e6e-432d-aba8-f3bdf4a3ada1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7687) },
                    { new Guid("baf35959-39c4-4035-bc12-5e2c9dd95b02"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7023), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("0fe424e1-769e-4443-b9c0-7529461fb02b"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7023) },
                    { new Guid("bd15d055-02fc-460a-b1f4-5a33eab9b80d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6810), "https://i.imgur.com/J6MinJn.jpeg", new Guid("8ec5f366-67e0-4311-a1bb-2bc6218749df"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6811) },
                    { new Guid("bda0260a-5825-475f-8dc7-74e0c08cd588"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7604), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("5d64819b-6e14-4300-9f1d-70234f54232c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7604) },
                    { new Guid("c137937a-7b57-4ea0-901e-84c12c02ac14"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7162), "https://placeimg.com/640/480/any", new Guid("f27aba4e-0dd0-49f7-a8a4-c12ceb3e43c9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7163) },
                    { new Guid("c18db406-6488-49f6-a04a-a2f79b6a0bc3"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7278), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("a4f1760b-48ac-49ee-bb5b-64c98783e6e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7278) },
                    { new Guid("c46df2bd-cee8-4f4d-92de-1ba65a7a885a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6678), "https://placeimg.com/640/480/any", new Guid("aa23694c-00f7-4185-9702-c8fd93526a70"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6679) },
                    { new Guid("c477e76e-42bd-4ab3-89f3-5844d96f203c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7775), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("05d0c36a-0fd0-4d81-83d7-67b5d96316fc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7776) },
                    { new Guid("c542addb-feaa-427d-811c-4d866a9893f0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7696), "https://placeimg.com/640/480/any", new Guid("d72edce2-0e6e-432d-aba8-f3bdf4a3ada1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7697) },
                    { new Guid("c5e514f1-b2a7-4c58-907a-6a16c04f7cfa"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7883), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("847435d7-d8b5-4501-9a16-f3a32786f659"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7884) },
                    { new Guid("c6c8789f-1319-4cfd-92fb-d56c7ecde44c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6639), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("1b3afb4b-5a43-4ecd-a735-8dc66bfc5105"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6640) },
                    { new Guid("c915d445-1c40-4197-a061-3c20da044586"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7906), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("615c4468-44ec-4a9d-a9b5-978864e37e37"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7907) },
                    { new Guid("c9296862-c3cd-460e-a812-5f23c0ab4142"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7762), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("207caeac-b110-4db6-a839-f0217b7542c0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7763) },
                    { new Guid("ca46415d-e862-4dd8-8c4f-bb99612977cc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7274), "https://i.imgur.com/WwKucXb.jpeg", new Guid("a4f1760b-48ac-49ee-bb5b-64c98783e6e8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7274) },
                    { new Guid("caa6c569-a2ac-4376-9c2d-1d278a475343"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7139), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("d32b5c89-6776-420d-b5e3-31612cd5061c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7140) },
                    { new Guid("ce29381f-9130-464e-8a6e-deacf370e2c1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6972), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("166ec928-8fa5-4a65-8e94-ecf223086697"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6973) },
                    { new Guid("cea758e4-6ef9-42ea-9350-b0027b2eae28"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8026), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("32fc81e8-88f6-4f63-9210-c34a7cf9d033"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8027) },
                    { new Guid("cfd21c25-f62e-452a-89ee-586ec70d2dc8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6504), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("c3e6dedb-928e-4611-aad4-ca312ebe80ef"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6505) },
                    { new Guid("cffb1903-9a21-401f-b8c2-a08993e33064"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7734), "https://i.imgur.com/WwKucXb.jpeg", new Guid("ce6d9918-3ef2-401d-a818-2db1b17c60ee"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7734) },
                    { new Guid("d050b452-99f3-466f-8d5b-b829a598cae2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7968), "https://placeimg.com/640/480/any", new Guid("ee87f96a-66dd-48fd-a65c-16a3cffd96db"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7969) },
                    { new Guid("d112ae80-62b9-4440-b471-51dcb43a2edb"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8148), "https://placeimg.com/640/480/any", new Guid("f7b7bb73-e690-4677-bde4-b7c521df2787"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8148) },
                    { new Guid("d1f155c0-da49-4492-b592-2dacff733ac4"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7116), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("02904361-4464-481f-a033-d26330da5a2e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7117) },
                    { new Guid("d2d65d04-59a7-47be-971d-1757ed1038ba"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6306), "https://placeimg.com/640/480/any", new Guid("9deb37cf-bf7a-4a70-8a8f-45513d827868"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6312) },
                    { new Guid("d3b5dd00-2637-41ac-a933-67084423dd66"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6710), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("27d6117c-bb11-474e-bebf-8645d8a4c1dc"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6711) },
                    { new Guid("d4e38ab5-c21e-4c86-b63d-a94969d8d3bd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6324), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("9deb37cf-bf7a-4a70-8a8f-45513d827868"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6325) },
                    { new Guid("d9a510a0-06ce-4281-915a-9e52f5e87570"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8112), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("00e4725f-3c80-4efd-84bc-d42586661139"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8113) },
                    { new Guid("da39fba6-9b24-4548-afd8-bc89d53d5ab6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7481), "https://i.imgur.com/mWwek7p.jpeg", new Guid("13152d29-82a9-4696-bfee-b5de82396696"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7482) },
                    { new Guid("dbeb3d71-532e-4ab6-8611-f8310ec5de2c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7938), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("78873241-9281-494e-80d2-8d28d0206e2d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7939) },
                    { new Guid("ddcf10fa-92d0-4485-84c3-98bdd6ad7707"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7367), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("e2c8bb63-0748-425c-80d6-5aa6828e2d4f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7368) },
                    { new Guid("de5de5fb-3756-4dda-b5fa-b22e42a9f832"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6793), "https://placeimg.com/640/480/any", new Guid("8ec5f366-67e0-4311-a1bb-2bc6218749df"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6795) },
                    { new Guid("de893ffb-d7eb-4f53-9cf7-c0db33dad4b2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7214), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("cd287de9-163b-4433-b40e-06bbb6b8b796"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7215) },
                    { new Guid("e0fd0d4f-f2b5-4603-b115-f2ea4dcffcfd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7616), "https://i.imgur.com/J6MinJn.jpeg", new Guid("ff9e48ab-9b83-46bc-a8c3-22ba141c79e0"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7617) },
                    { new Guid("e2043328-fd3c-4f3d-a0a2-60173ae692d6"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6932), "https://i.imgur.com/J6MinJn.jpeg", new Guid("23c38bf9-2bf7-47dd-86af-bfd78aae4c04"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6933) },
                    { new Guid("e401aab2-9221-4e93-a384-c83577ee2f24"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7895), "https://i.imgur.com/mWwek7p.jpeg", new Guid("00a2d36b-38f9-4c89-8896-6ca91a7cde8d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7895) },
                    { new Guid("e6c4fb22-de12-402e-ac87-0735248dc6c8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8009), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("af018b8a-5678-48a5-bbd8-5fe97c828343"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8010) },
                    { new Guid("e928019d-7eaf-4175-85dd-009f2b8a4ca2"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7867), "https://i.imgur.com/J6MinJn.jpeg", new Guid("d504c2b1-5419-4f76-a01e-6461ab6169be"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7868) },
                    { new Guid("ea9d40d1-4fcb-42c4-a5c8-02cfe2d0fbc5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7226), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("e9fc2e2c-5afe-4f66-9de9-0b7e1f9049be"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7227) },
                    { new Guid("eaa9504b-0b6e-42ae-b8f3-d6c548227cf9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6776), "https://i.imgur.com/NLn4e7S.jpeg", new Guid("2920eb37-02a9-428c-b975-74fb6959a42d"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6776) },
                    { new Guid("ed7444ba-9f21-4a4f-953b-5bdee252c7e5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6731), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("3ae045c8-32c9-4017-b682-aa632733d979"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6731) },
                    { new Guid("ee3d8a72-5859-477d-9f27-5f9aacea4534"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7382), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("e2c8bb63-0748-425c-80d6-5aa6828e2d4f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7383) },
                    { new Guid("ee74799e-28cf-4c32-aec7-69d9bd1ebe9c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8016), "https://i.imgur.com/J6MinJn.jpeg", new Guid("af018b8a-5678-48a5-bbd8-5fe97c828343"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(8016) },
                    { new Guid("eeb0c1ba-dc63-4723-8481-049df2d8f0fd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7406), "https://placeimg.com/640/480/any", new Guid("3c644029-46e8-424d-b82c-fa4238be9b4e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7406) },
                    { new Guid("eecd3c87-8034-4405-8ae0-a19833db5075"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7527), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("4016601a-e208-4589-9491-2858c5f10807"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7527) },
                    { new Guid("f094e091-588c-4399-b0bc-4f32de1d0370"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7230), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("e9fc2e2c-5afe-4f66-9de9-0b7e1f9049be"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7231) },
                    { new Guid("f22369d7-8ebd-4bf9-b08b-9bdd68339bdd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7644), "https://placeimg.com/640/480/any", new Guid("90302e91-c81f-41d8-bd5d-ac516f4139e5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7645) },
                    { new Guid("f2947a98-63e6-466e-8ff5-94aa843b8751"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7429), "https://i.imgur.com/mWwek7p.jpeg", new Guid("fda86b05-0079-4256-8cdd-65197656b89c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7430) },
                    { new Guid("f2d210aa-3bb1-4df1-9e6f-f34720319ef8"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6722), "https://placeimg.com/640/480/any", new Guid("d1810ab0-11d6-40ec-9378-234b0cb9bd83"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6722) },
                    { new Guid("f31e58ef-73b6-4eab-96de-afdfa8e7c886"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7986), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("32ac6cc2-d769-40ac-96ab-ddb1c0a16606"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7986) },
                    { new Guid("f4398025-9674-4b80-b03a-66ad6102b3b5"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7377), "https://i.imgur.com/WwKucXb.jpeg", new Guid("e2c8bb63-0748-425c-80d6-5aa6828e2d4f"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7377) },
                    { new Guid("f7bab5f3-7130-4d4d-a52b-168a4c39865e"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7174), "https://i.imgur.com/JQRGIc2.jpeg", new Guid("f27aba4e-0dd0-49f7-a8a4-c12ceb3e43c9"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7174) },
                    { new Guid("f7f399ff-deb5-473c-a798-b55d152169a1"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7975), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("32ac6cc2-d769-40ac-96ab-ddb1c0a16606"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7976) },
                    { new Guid("f8079e25-cbfc-4f99-b2fc-209a32474127"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7156), "https://i.imgur.com/J6MinJn.jpeg", new Guid("d32b5c89-6776-420d-b5e3-31612cd5061c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7157) },
                    { new Guid("fa7b5133-6e33-48f1-8c05-cec163508d46"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7499), "https://i.imgur.com/mWwek7p.jpeg", new Guid("0cdb8bd1-cf78-4d3c-94aa-ca8afe4b4319"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7500) },
                    { new Guid("fa8e2b07-2f29-441a-b8a8-15777f315d9a"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6401), "https://i.imgur.com/J6MinJn.jpeg", new Guid("dd8e6825-4e35-45ff-8847-a8223344f676"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(6402) },
                    { new Guid("fb807967-712f-44d8-a446-f0f30019c26c"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7464), "https://i.imgur.com/QkIa5tT.jpeg", new Guid("13152d29-82a9-4696-bfee-b5de82396696"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7465) },
                    { new Guid("ff899f0e-7e37-44a2-803e-65b5e3708331"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7187), "https://placeimg.com/640/480/any", new Guid("f41e0c33-e149-436f-82fd-f6847aad0274"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7188) },
                    { new Guid("ffa7d9fd-55b4-452f-9068-b0555f9e7dfd"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7515), "https://i.imgur.com/kKc9A5p.jpeg", new Guid("4016601a-e208-4589-9491-2858c5f10807"), new DateTime(2023, 12, 25, 0, 28, 41, 900, DateTimeKind.Local).AddTicks(7516) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_user_id",
                table: "addresses",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_images_product_id",
                table: "images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_products_product_id",
                table: "orders_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "orders_products");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
