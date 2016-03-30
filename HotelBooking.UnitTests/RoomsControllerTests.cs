using NUnit.Framework;
using HotelBooking.DAL.Repositories;
using HotelBooking.Models;
using HotelBooking.Controllers;
using NSubstitute;
using System;

namespace HotelBooking.UnitTests
{
    class RoomsControllerTests
    {
        [Test]
        public void DeleteConfirmed_WhenIdIsLargerThanZero_RemoveIsCalled()
        {
            // This test demonstrates interaction testing using NSubstitute to create
            // a mock object.

            // Arrange

            // Create a mock object
            IRepository<Room> fakeRoomRepos = Substitute.For<IRepository<Room>>();
            // Create RoomsController instance and inject the fake RoomRepository
            RoomsController controller = new RoomsController(fakeRoomRepos);
            int id = 1;

            // Act

            // When we want to test a controller action the test project must have
            // a reference to the System.Web.Mvc assembly. I did this by adding the
            // NuGet package "Microsoft.AspNet.Mvc" to the test project.
            controller.DeleteConfirmed(id);

            // Assert

            // Assert against the mock object
            fakeRoomRepos.Received().Remove(id);
        }

        [Test]
        //[Ignore]
        public void DeleteConfirmed_WhenIdIsLessThanOne_RemoveIsNotCalled()
        {
            // Arrange
            IRepository<Room> fakeRoomRepos = Substitute.For<IRepository<Room>>();
            RoomsController controller = new RoomsController(fakeRoomRepos);
            int id = 0;

            // Act
            controller.DeleteConfirmed(id);

            // Assert
            fakeRoomRepos.DidNotReceive().Remove(id);
        }

        [Test]
        [Ignore]
        public void DeleteConfirmed_WhenIdIsLessThanOne_RemoveThrowsException()
        {
            // Arrange
            IRepository<Room> fakeRoomRepos = Substitute.For<IRepository<Room>>();
            fakeRoomRepos.When(repos => repos.Remove(Arg.Is(0))).
                Do(context => { throw new Exception("Room id cannot be less than 1"); });
            RoomsController controller = new RoomsController(fakeRoomRepos);

            // Assert
            Assert.Throws<Exception>(() => controller.DeleteConfirmed(0));
        }
    }
}
