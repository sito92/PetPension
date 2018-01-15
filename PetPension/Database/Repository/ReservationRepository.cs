using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.IRepository;
using Database.Models;

namespace Database.Repository
{
    public class ReservationRepository : GenericRepository<MainContext, Reservation>, IReservationRepository
    {
        public void AddReservation(Reservation reservation)
        {
           Add(reservation);
            Save();
        }
    }
}
