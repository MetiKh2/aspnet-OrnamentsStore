using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Dtos.Order
{
    public class AddProductToOrderDto
    {
        public long OrnamentId { get; set; }
         
        public int Count { get; set; }
        public int Size { get; set; }
    }
}
