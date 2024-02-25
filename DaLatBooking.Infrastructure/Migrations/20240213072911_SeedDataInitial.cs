using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DaLatBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, "Phòng gỗ của chúng tôi là một kỳ quan đích thực của sự sáng tạo và tự nhiên. Tấm ván gỗ tự nhiên được sử dụng cho cả tường và sàn, tạo ra một không gian ấm áp và mộc mạc. Chiếc giường gỗ chắc chắn được bố trí giữa phòng với ga trải giường mềm mại, tạo điều kiện lý tưởng cho một giấc ngủ ngon lành", "https://placehold.co/600x400", "Phòng Gỗ", 4, 800.0, 500, null },
                    { 2, null, "Phòng đá của chúng tôi là một biểu tượng của sức mạnh và sự vững chãi của thiên nhiên. Tường và sàn được lát bằng các tấm đá tự nhiên, tạo ra một không gian mát mẻ và bền vững. Không gian rộng rãi và thoải mái với đồ nội thất đơn giản nhưng đầy tính thẩm mỹ", "https://placehold.co/600x401", "Phòng Đá", 4, 1200.0, 600, null },
                    { 3, null, "Phòng homestay bình thường của chúng tôi được thiết kế để mang lại sự tiện nghi và thoải mái cho du khách. Với không gian rộng rãi và ánh sáng tự nhiên, phòng homestay mang lại cảm giác ấm cúng và thân thiện", "https://placehold.co/600x402", "Phòng Thường", 2, 400.0, 300, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
