using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Model
{


    public class Jobfunction
    {
        public int JobfunctionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }



        public Jobfunction() { }

        public override string ToString()
        {
            return $"{Name},{Description}";
        }
    }


}
