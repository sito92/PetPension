using BusinessLogic.IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.IRepository;
using Database.Models;

namespace BusinessLogic.BusinessLogic
{
    public class ReserverationTimesBL : IReserverationTimesBL
    {
        private IReservationTimesRepository reservationTimesRepo;
        public ReserverationTimesBL(IReservationTimesRepository _reservationTimes)
        {
            reservationTimesRepo = _reservationTimes;
        }
        public List<ReservationTime> GetClosestReservetionTimes(int roomType, DateTime startDate, int duration)
        {

            var reservationTimes = reservationTimesRepo.GetReservationTimesForRoomType(roomType);
            return reservationTimes;
        }
    }
}
