 
namespace Ornaments.Core.Dtos.Ornament.Comment
{
	public class AddCommentDto
	{
        public string? UserId { get; set; }
        public string   Text{ get; set; }
        public long OrnamentId { get; set; }
    }
}
