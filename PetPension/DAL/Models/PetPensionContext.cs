using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PetPensionContext: DbContext
    {
        public PetPensionContext(): base("PetPension")
        {
            
        }

        public DbSet<Pet> Pets { get; set; }
    }
}
