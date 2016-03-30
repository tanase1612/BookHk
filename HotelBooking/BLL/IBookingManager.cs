using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Models;

namespace HotelBooking.BLL
{
    public interface IBookingManager
    {
        Booking CreateBooking(Booking booking);
        int FindAvailableRoom(DateTime startDate, DateTime endDate);
        List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate);
    }
}
