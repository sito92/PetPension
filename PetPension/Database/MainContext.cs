using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database
{
    public class MainContext : DbContext
    {
        public MainContext() : base("name=")
        {

        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<ReservationTime> ReservationTimes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
