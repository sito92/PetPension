using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database.IRepository
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation reservation);
    }
}
