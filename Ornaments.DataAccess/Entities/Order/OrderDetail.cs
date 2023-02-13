using Ornaments.DataAccess.Entities.Ornaments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.DataAccess.Entities.Order
{
    public class OrderDetail:BaseEntity
    {
        #region props
        public long OrderId { get; set; }
        public long OrnamentId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        #endregion
        #region rel
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("OrnamentId")]
        public Ornament Ornament { get; set; }
        #endregion
    }
}
