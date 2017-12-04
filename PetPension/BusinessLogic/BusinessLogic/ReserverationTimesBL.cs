using BusinessLogic.IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace BusinessLogic.BusinessLogic
{
    public class ReserverationTimesBL : IReserverationTimesBL
    {
        private IReserverationTimesBL reservationTimesRepo;
        public ReserverationTimesBL(IReserverationTimesBL _reservationTimes)
        {
            reservationTimesRepo = _reservationTimes;
        }
        public List<ReservationTime> GetClosestReservetionTimes(int roomType, DateTime startDate, int duration)
        {

            throw new NotImplementedException();
        }
    }
}
