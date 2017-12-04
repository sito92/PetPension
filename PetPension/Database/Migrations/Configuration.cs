using Database.Models;

namespace Database.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MainContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var pets = GetPets();
            context.Pets.AddOrUpdate(x => x.Id, pets.ToArray());
            context.Rooms.AddOrUpdate(GetRooms(pets).ToArray());
            
        }
        private List<Models.Room> GetRooms(List<Pet> pets)
        {
            var result = new List<Models.Room>();
            for (int i=0;i<13; i++)
            {
                result.Add(new Models.Room()
                {
                    RoomTypeId = i % 3,
                    Reservations = new List<Reservation>()
                    {
                        new Reservation(){CreatedTime = DateTime.Now,Pet = pets.FirstOrDefault(),ReservationTime = new ReservationTime()
                        {
                            From = DateTime.Now.AddDays(i),
                            To = DateTime.Now.AddDays(i+2)
                            
                        }},
                        new Reservation(){CreatedTime = DateTime.Now,Pet = pets.FirstOrDefault(),ReservationTime = new ReservationTime()
                        {
                            From = DateTime.Now.AddDays(i+3),
                            To = DateTime.Now.AddDays(i+5)

                        }},
                        new Reservation(){CreatedTime = DateTime.Now,Pet = pets.FirstOrDefault(),ReservationTime = new ReservationTime()
                        {
                            From = DateTime.Now.AddDays(i+6),
                            To = DateTime.Now.AddDays(i+8)

                        }},
                        new Reservation(){CreatedTime = DateTime.Now,Pet = pets.FirstOrDefault(),ReservationTime = new ReservationTime()
                        {
                            From = DateTime.Now.AddDays(i+9),
                            To = DateTime.Now.AddDays(i+11)

                        }}
                    }
                });
            }
            return result;
        }

        private List<Pet> GetPets()
        {
            var result = new List<Pet>();
            for (int i = 1; i < 5; i++)
            {
              result.Add(new Pet()
              {
                  Id = i,
                  Name = $"{i} - pet"
              });
            }
            return result;
        }
    }
}
