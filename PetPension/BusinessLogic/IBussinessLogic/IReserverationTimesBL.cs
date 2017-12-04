using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IBussinessLogic
{
    public interface IReserverationTimesBL
    {
        List<ReservationTime> GetClosestReservetionTimes(int roomType,DateTime startDate,int duration);
    }
}
