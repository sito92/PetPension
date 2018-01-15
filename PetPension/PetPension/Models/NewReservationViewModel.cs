using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetPension.Models
{
    public class NewReservationViewModel
    {
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime MaxStarTime { get; set; }
        public DateTime MaxEndTime { get; set; }
    }
}