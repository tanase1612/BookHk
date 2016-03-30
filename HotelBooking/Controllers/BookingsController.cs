using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HotelBooking.Models;
using HotelBooking.DAL.Repositories;
using HotelBooking.DAL;
using HotelBooking.BLL;

namespace HotelBooking.Controllers
{
    public class BookingsController : Controller
    {
        private IRepository<Booking> bookingRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Room> roomRepository;
        private IBookingManager bookingManager;
        private IBookingViewModel bookingViewModel;

        public BookingsController(IRepository<Booking> br, IRepository<Customer> cr, 
            IRepository<Room> rr, IBookingManager bm, IBookingViewModel bvm)
        {
            bookingRepository = br;
            customerRepository = cr;
            roomRepository = rr;
            bookingManager = bm;
            bookingViewModel = bvm;
        }

        // GET: Bookings
        public ActionResult Index(int? id)
        {
            bookingViewModel.YearToDisplay = (id == null) ? DateTime.Today.Year : id.Value;
            return View(bookingViewModel);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingRepository.Get(id.Value);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(customerRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartDate,EndDate,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                Booking newBooking = bookingManager.CreateBooking(booking);
                if (newBooking != null)
                    return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(customerRepository.GetAll(), "Id", "Name", booking.CustomerId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingRepository.Get(id.Value);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(customerRepository.GetAll(), "Id", "Name", booking.CustomerId);
            ViewBag.RoomId = new SelectList(roomRepository.GetAll(), "Id", "Description", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartDate,EndDate,IsActive,CustomerId,RoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                bookingRepository.Edit(booking);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(customerRepository.GetAll(), "Id", "Name", booking.CustomerId);
            ViewBag.RoomId = new SelectList(roomRepository.GetAll(), "Id", "Description", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingRepository.Get(id.Value);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bookingRepository.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
