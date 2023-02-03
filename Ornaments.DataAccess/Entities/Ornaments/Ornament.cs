using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ornaments.DataAccess.Entities.Ornaments
{
    public class Ornament:BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Invertory { get; set; }
        public bool IsDisplay { get; set; } = true;
        public bool IsInMainPage{ get; set; } = true;
        public bool IsSlider { get; set; } = false;
        public double Price { get; set; }
        public long ViewCount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public long CategoryId { get; set; }

    }
}
