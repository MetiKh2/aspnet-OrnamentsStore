
using Ornaments.DataAccess.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ornaments.DataAccess.Entities.Ornaments
{
    public class Comment:BaseEntity
    {
        public string UserId { get; set; }
        public long OrnamentId { get; set; }
        [Required]
        public string Text { get; set; }

        #region Rel
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("OrnamentId")]
        public Ornament Ornament{ get; set; }
        #endregion
    }
}
