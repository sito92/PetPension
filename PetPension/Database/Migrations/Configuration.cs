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


            context.Rooms.AddOrUpdate(GetRooms().ToArray());
        }
        private List<Models.Room> GetRooms()
        {
            var result = new List<Models.Room>();
            for (int i=0;i<13; i++)
            {
                result.Add(new Models.Room()
                {
                    RoomTypeId = i % 3
                });
            }
            return result;
        }
    }
}
