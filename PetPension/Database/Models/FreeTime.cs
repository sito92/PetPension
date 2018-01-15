using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class FreeTime
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid Id => Guid.NewGuid();
        public int AllDay => 1;
    }
}
