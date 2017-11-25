using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
