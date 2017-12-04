using Database.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Repository
{
    public class ReservationTimesRepository : GenericRepository<MainContext,ReservationTime>, IReservationTimesRepository
    {
        public List<ReservationTime> GetReservationTimesForRoomType(int roomType)
        {
            var query = from reservations in Context.Reservations
                        join room in Context.Rooms on reservations.RoomId equals room.Id
                        join rt in Context.ReservationTimes on reservations.ReservationTimeId equals rt.Id
                        select rt;

            return query.ToList();
        }
    }
}
