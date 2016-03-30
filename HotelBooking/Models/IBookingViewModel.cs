using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Models
{
    public interface IBookingViewModel
    {
        List<DateTime> FullyOccupiedDates { get; }
        int YearToDisplay { get; set; }
        string GetMonthName(int month);
        bool DateIsOccupied(int year, int month, int day);

    }
}
