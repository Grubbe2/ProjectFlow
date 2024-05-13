using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Model
{
    public class Attribute
    {
        public string Characteristics { get; set; }
        public string Service { get; set; }

        public int AttributeId { get; set; }

        public override string ToString()
        {
            return $"{Characteristics},{Service}";
        }
    }
}
