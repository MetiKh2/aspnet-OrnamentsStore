using Ornaments.DataAccess.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Dtos.Ornament.Comment
{
    public class CommentsDto
    {
        public long Id{ get; set; }
        public DateTime?  CreateDate { get; set; }
        public string UserName { get; set; }
        public string OrnamentName { get; set; }
        public long OrnamentId{ get; set; }
        public string Text { get; set; }
    }
}
