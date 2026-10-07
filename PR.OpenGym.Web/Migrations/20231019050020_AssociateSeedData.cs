using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PR.OpenGym.Web.Migrations
{
    /// <inheritdoc />
    public partial class AssociateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "257020d9-bc80-4821-a6ca-ffef7c787dfa",
                column: "ConcurrencyStamp",
                value: "837e6317-0af0-429b-8de6-cdf9f8bbe90d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28fc5ae8-c637-4bfd-8aa4-23c785a28bfb",
                column: "ConcurrencyStamp",
                value: "cabdf966-1251-4778-8af0-3c7bfa0406a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20e6c427-d499-4fb3-b53b-0e44d99767b7",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "092eb93c-78db-44d7-a6d7-8923a7375b00", "USER@316FITNESS.COM", "AQAAAAEAACcQAAAAEAiani0i3CtThr1lPh2pWWd6EJAlAtid2MQtGYoLg5i0OlfVNxP5HpxEZJOBZ8Ftqg==", "cca4e424-d805-4a7f-904c-b448dc5399ad", "user@316fitness.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57c7963f-c1fb-48b9-847a-38deadd11453",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "74573664-12a1-490b-ab9d-70f9823d61a3", "ADMIN@316FITNESS.COM", "AQAAAAEAACcQAAAAEAiani0i3CtThr1lPh2pWWd6EJAlAtid2MQtGYoLg5i0OlfVNxP5HpxEZJOBZ8Ftqg==", "388f0561-eaed-4189-90dd-caa303d6102e", "admin@316fitness.com" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "3:16 Fitness - Río Mayo Delta");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOn", "Description", "Discriminator", "ModifiedOn", "Name", "Period", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(8802), "30 días", "Membership", new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(8898), "Mensual", 30, 499.99m },
                    { 2, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(8910), "15 días", "Membership", new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(8914), "Quincenal", 15, 299.99m }
                });

            migrationBuilder.InsertData(
                table: "AssociateMemberships",
                columns: new[] { "Id", "CreatedOn", "From", "MembershipId", "MembershipStatus", "ModifiedOn", "To" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298), new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298), 1, 0, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298), new DateTime(2122, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298) },
                    { 2, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298), new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298), 1, 0, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298), new DateTime(2122, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9298) }
                });

            migrationBuilder.InsertData(
                table: "Associates",
                columns: new[] { "Id", "Age", "AssociateMembershipId", "BranchId", "CreatedOn", "DateBirth", "Email", "Facebook", "FirstName", "Gender", "ImgPath", "LastName", "ModifiedOn", "Phone", "Status" },
                values: new object[,]
                {
                    { 1, null, 1, null, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9185), null, "admin@316fitness.com", null, "Admin", null, null, null, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9185), null, 1 },
                    { 2, null, 2, null, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9185), null, "no-mail@316fitness.com", null, "Visita", null, null, null, new DateTime(2023, 10, 18, 23, 0, 19, 951, DateTimeKind.Unspecified).AddTicks(9185), null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Associates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Associates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AssociateMemberships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AssociateMemberships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "257020d9-bc80-4821-a6ca-ffef7c787dfa",
                column: "ConcurrencyStamp",
                value: "c151656c-f62e-41ea-a78c-c1fa37d2ad22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28fc5ae8-c637-4bfd-8aa4-23c785a28bfb",
                column: "ConcurrencyStamp",
                value: "2deacb4b-698e-4c82-8f94-2c83df8a5277");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20e6c427-d499-4fb3-b53b-0e44d99767b7",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "59a601cd-0152-4c5a-b109-47f0bc1e3fd2", "USER@GETUPFITNESS.COM", "AQAAAAEAACcQAAAAEEqvs6OwftVnu+WNtqA+VGav/VJoIPz3tbrK2j1YI51JxwfqRcSUZXsmvAybNWdqlg==", "186e59ba-9f56-467c-a5b7-bac9b0763b00", "user@getupfitness.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57c7963f-c1fb-48b9-847a-38deadd11453",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2b62ecd6-b4c5-4a2d-829d-2bd93eb32a41", "ADMIN@GETUPFITNESS.COM", "AQAAAAEAACcQAAAAEEqvs6OwftVnu+WNtqA+VGav/VJoIPz3tbrK2j1YI51JxwfqRcSUZXsmvAybNWdqlg==", "c829f426-bf66-4209-a5fd-0bbb4931f0ef", "admin@getupfitness.com" });

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Fitness Gym Delta");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOn", "Description", "Discriminator", "ModifiedOn", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7969), null, "Product", new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7970), "Proteina", 199.99m },
                    { 2, new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7972), null, "Product", new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7972), "Inscripcion", 99.99m },
                    { 3, new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7973), null, "Product", new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7973), "Clases", 99.99m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOn", "Description", "Discriminator", "ModifiedOn", "Name", "Period", "Price" },
                values: new object[,]
                {
                    { 4, new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7888), null, "Membership", new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7891), "Mensual", 30, 499.99m },
                    { 5, new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7898), null, "Membership", new DateTime(2023, 10, 15, 1, 28, 24, 65, DateTimeKind.Utc).AddTicks(7898), "Quincenal", 15, 299.99m }
                });
        }
    }
}
