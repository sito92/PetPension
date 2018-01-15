using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.IRepository
{
    public interface IReservationTimesRepository
    {
        List<ReservationTime> GetReservationTimesForRoomType(int roomType);       
    }
}
