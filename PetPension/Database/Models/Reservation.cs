using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Reservation
    {
        public int Id { get; set; }      
        public DateTime CreatedTime { get; set; }
        public ReservationTime ReservationTime { get; set; }
        public Pet Pet { get; set; }
    }
}
