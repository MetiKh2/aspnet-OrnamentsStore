
using System.ComponentModel.DataAnnotations;

namespace Ornaments.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
    }
}
