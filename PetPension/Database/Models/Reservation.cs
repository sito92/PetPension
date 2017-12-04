using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Reservation
    {
        public int Id { get; set; }      
        public DateTime CreatedTime { get; set; }

        [Key, ForeignKey("ReservationTime")]
        public int ReservationTimeId { get; set; }

        public ReservationTime ReservationTime { get; set; }
        public virtual Pet Pet { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
