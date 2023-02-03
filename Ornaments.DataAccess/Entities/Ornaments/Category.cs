 

namespace Ornaments.DataAccess.Entities.Ornaments
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<Ornament> Ornaments{ get; set; }
    }
}
