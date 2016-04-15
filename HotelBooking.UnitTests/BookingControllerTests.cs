using System;
using NUnit.Framework;
using HotelBooking.BLL;
using HotelBooking.DAL.Repositories;
using HotelBooking.DAL;
using HotelBooking.Models;
using System.Collections.Generic;
using NSubstitute;
using HotelBooking.Controllers;

namespace HotelBooking.UnitTests
{
    class BookingControllerTests
    {

        //private IBookingManager bookingManager;
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

        //[Test]
        //[Ignore]
        //public void Details_IdIsLessThenOne_DetailsThrowsException()
        //{
        //    //Arrange
        //    IRepository<Room> fakeCustRepos = Substitute.For<IRepository<Room>>();
        //    IRepository<Customer> fakeRoomRepos = Substitute.For<IRepository<Customer>>();
        //    IRepository<Booking> fakeBookingRepos = Substitute.For<IRepository<Booking>>();
        //    IBookingManager iBookViewModel;
            

        //    //fakeBookingRepos.When(repos => repos.Get(Arg.Is(0))).
        //    //    Do(context => { throw new Exception("Room id cannot be less then 1"); });
        //    //BookingsController controller = new BookingsController(fakeBookingRepos,fakeRoomRepos, fakeCustRepos);
        //    ////Assert
        //    //Assert.Throws<Exception>(() => controller.Details(0));
        //}
    }
}
