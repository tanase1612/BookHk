﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Models;
using HotelBooking.DAL;
using HotelBooking.DAL.Repositories;

namespace HotelBooking.BLL
{
    public class BookingManager : IBookingManager
    {
        private IRepository<Booking> bookingRepository;
        private IRepository<Room> roomRepository;


        // Constructor injection
        public BookingManager(IRepository<Booking> bookingRepository, IRepository<Room> roomRepository)
        {
            this.bookingRepository = bookingRepository;
            this.roomRepository = roomRepository;
        }

        /// <summary>
        /// This method is used to create a booking for a hotel room
        /// </summary>
        /// <param name="booking">The booking to be created</param>
        /// <returns>The new Booking object or null if the booking could not be created</returns>
        public Booking CreateBooking(Booking booking)
        {
            int roomId = FindAvailableRoom(booking.StartDate, booking.EndDate);

            if (roomId >= 0)
            {
                booking.RoomId = roomId;
                booking.IsActive = true;
                Booking newBooking = bookingRepository.Add(booking);
                return newBooking;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method is used to find an available room for a specified period
        /// </summary>
        /// <param name="startDate">Start date of the specified period</param>
        /// <param name="endDate">End date of the specified period</param>
        /// <returns>Id of the room or -1 if an available room could not be found</returns>
        public int FindAvailableRoom(DateTime startDate, DateTime endDate)
        {
            if (startDate <= SystemTime.Today || startDate > endDate)
                throw new ArgumentException("The start date cannot be in the past or later than the end date.");

            foreach (var room in roomRepository.GetAll())
            {
                if (!bookingRepository.GetAll().Any(
                        b => b.RoomId == room.Id && b.IsActive &&
                        (startDate >= b.StartDate && startDate <= b.EndDate ||
                        endDate >= b.StartDate && endDate <= b.EndDate)
                    ))
                {
                    return room.Id;
                }
            }
            return -1;
        }

        /// <summary>
        /// This method is used to find the dates where all rooms are occupied
        /// </summary>
        /// <param name="startDate">Start date for the search scope</param>
        /// <param name="endDate">End date for the search scope</param>
        /// <returns>The dates that are fully occupied as a list of DateTime objecs</returns>
        public List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("The start date cannot be later than the end date.");

            List<DateTime> fullyOccupiedDates = new List<DateTime>();
            int noOfRooms = roomRepository.GetAll().Count();
            var bookings = bookingRepository.GetAll();

            if (bookings.Any())
            {
                for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
                {
                    var noOfBookings = from b in bookings
                                        where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                        select b;
                    if (noOfBookings.Count() >= noOfRooms)
                        fullyOccupiedDates.Add(d);
                }
            }
            return fullyOccupiedDates;
        }

    }
}