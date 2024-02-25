using DaLatBooking.Domain.Entities;
using DaLatBooking.Web.ViewModels;

namespace DaLatBooking.Application.Common.Utility
{
    public static class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";

        public const string StatusPending = "Chưa giải quyết";
        public const string StatusApproved = "Đã phê duyệt";
        public const string StatusCheckedIn = "Đã đặt phòng";
        public const string StatusCompleted = "Hoàn thành";
        public const string StatusCancelled = "Huỷ bỏ";
        public const string StatusRefunded = "Đã hoàn tiền";


        public static int VillaRoomsAvailable_Count(int villaId,
           List<VillaNumber> villaNumberList, DateOnly checkInDate, int nights,
             List<Booking> bookings)
        {
            List<int> bookingInDate = new();
            int finalAvailableRoomForAllNights = int.MaxValue;
            var roomsInVilla = villaNumberList.Where(x => x.VillaId == villaId).Count();

            for (int i = 0; i < nights; i++)
            {
                var villasBooked = bookings.Where(u => u.CheckInDate <= checkInDate.AddDays(i)
                && u.CheckOutDate > checkInDate.AddDays(i) && u.VillaId == villaId);

                foreach (var booking in villasBooked)
                {
                    if (!bookingInDate.Contains(booking.Id))
                    {
                        bookingInDate.Add(booking.Id);
                    }
                }

                var totalAvailableRooms = roomsInVilla - bookingInDate.Count;
                if (totalAvailableRooms == 0)
                {
                    return 0;
                }
                else
                {
                    if (finalAvailableRoomForAllNights > totalAvailableRooms)
                    {
                        finalAvailableRoomForAllNights = totalAvailableRooms;
                    }
                }
            }

            return finalAvailableRoomForAllNights;
        }

        public static RadialBarChartDto GetRadialCartDataModel(int totalCount, double currentMonthCount, double prevMonthCount)
        {
            RadialBarChartDto radialBarChartVM = new();

            int increaseDecreaseRatio = 100;

            if (prevMonthCount != 0)
            {
                increaseDecreaseRatio = Convert.ToInt32((currentMonthCount - prevMonthCount) / prevMonthCount * 100);
            }

            radialBarChartVM.TotalCount = totalCount;
            radialBarChartVM.CountInCurrentMonth = Convert.ToInt32(currentMonthCount);
            radialBarChartVM.HasRatioIncreased = currentMonthCount > prevMonthCount;
            radialBarChartVM.Series = new int[] { increaseDecreaseRatio };

            return radialBarChartVM;
        }
    }
}
