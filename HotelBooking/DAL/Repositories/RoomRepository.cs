using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelBooking.Models;
using System.Data.Entity;

namespace HotelBooking.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        public Room Add(Room entity)
        {
            using (HotelBookingContext db = new HotelBookingContext())
            {
                Room newRoom = db.Rooms.Add(entity);
                db.SaveChanges();
                return newRoom;
            }
        }

        public void Edit(Room entity)
        {
            using (HotelBookingContext db = new HotelBookingContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Room Get(int id)
        {
            using (HotelBookingContext db = new HotelBookingContext())
            {
                return db.Rooms.FirstOrDefault(r => r.Id == id);
            }
        }

        public IEnumerable<Room> GetAll()
        {
            using (HotelBookingContext db = new HotelBookingContext())
            {
                return db.Rooms.ToList();
            }
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new Exception("Room id cannot be less than 1");
            using (HotelBookingContext db = new HotelBookingContext())
            {
                var room = db.Rooms.FirstOrDefault(r => r.Id == id);
                db.Rooms.Remove(room);
                db.SaveChanges();
            }
        }
    }
}