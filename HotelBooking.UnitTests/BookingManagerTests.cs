using System;
using NUnit.Framework;
using HotelBooking.BLL;
using HotelBooking.DAL.Repositories;
using HotelBooking.DAL;
using HotelBooking.Models;
using System.Collections.Generic;
using NSubstitute;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        [SetUp]
        public void Setup()
        {
            SystemTime.Set(new DateTime(2002, 1, 1));
        }

        [TearDown]
        public void TearDown()
        {
            SystemTime.Reset();
        }

        [Test]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsException()
        {
            BookingManager manager = CreateBookingManager();
            DateTime date = SystemTime.Today;
            Assert.Catch<Exception>(() => manager.FindAvailableRoom(date, date));
        }

        [Test]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            BookingManager manager = CreateBookingManager();
            DateTime date = SystemTime.Today.AddDays(21);
            int roomId = manager.FindAvailableRoom(date, date);
            Assert.AreNotEqual(-1, roomId);
        }
        [Test]
        public void GetFullyOccupiedDates_RoomsOccupiedOnDate_RoomIdNotMinusOne()
        {
            BookingManager manager = CreateBookingManager();
            DateTime date = SystemTime.Today.AddDays(21);
            List<DateTime> dates = manager.GetFullyOccupiedDates(date, date);
            Assert.AreNotEqual(-1, dates);
        }

        private BookingManager CreateBookingManager()
        {
            DateTime start = SystemTime.Today.AddDays(10);
            DateTime end = SystemTime.Today.AddDays(20);
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2 },
            };
            List<Room> rooms = new List<Room>
            {
                new Room { Id = 1 },
                new Room { Id = 2 }
            };

            // Create a fake BookingRepository using NSubstitute
            IRepository<Booking> bookingRepository = Substitute.For<IRepository<Booking>>();
            // Set a return value for GetAll() 
            bookingRepository.GetAll().Returns(bookings);
            // Set a return value for Get() - not used
            bookingRepository.Get(2).Returns(bookings[1]);

            // Create a fake RoomRepository using NSubstitute
            IRepository<Room> roomRepository = Substitute.For<IRepository<Room>>();
            // Set a return value for GetAll() 
            roomRepository.GetAll().Returns(rooms);

            return new BookingManager(bookingRepository, roomRepository);
        }
        [Test]
        public void FindAvailableRoom_StartDateNotBeforeEndDate_ThrowsException()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = SystemTime.Today.AddDays(20).AddMonths(4).AddYears(14);
            DateTime end = SystemTime.Today.AddDays(19).AddMonths(4).AddYears(14);
            Assert.Catch<Exception>(() => manager.FindAvailableRoom(start, end));
        }

        [Test]
        public void CreateBooking_CreateBooking_Pass()
        {
            IRepository<Booking> bookingRepository = Substitute.For<IRepository<Booking>>();
            IRepository<Room> roomRepository = Substitute.For<IRepository<Room>>();
            BookingManager booking = new BookingManager(bookingRepository, roomRepository);
            DateTime start = SystemTime.Today.AddDays(20).AddMonths(4).AddYears(14);
            DateTime end = SystemTime.Today.AddDays(22).AddMonths(4).AddYears(14);
            Booking toBook = new Booking { Id = 3, StartDate = start, EndDate = end, CustomerId = 1};
            List<Room> rooms = new List<Room>
            {
                new Room { Id = 1 },
                new Room { Id = 2 }
            };
            roomRepository.GetAll().Returns(rooms);
            booking.CreateBooking(toBook);
            
            bookingRepository.Received().Add(toBook);
        }
    }
}
