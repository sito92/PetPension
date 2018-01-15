using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.IRepository;
using Database.Models;

namespace Database.Repository
{
    public class RoomsRepository:GenericRepository<MainContext,Room>,IRoomsRepostitory
    {
        public List<Room> GetRoomsPerType(int roomTypeId)
        {
            var query = from rooms in Context.Rooms
                        where rooms.RoomTypeId == roomTypeId
                select rooms;

            return query.ToList();
        }
    }
}
