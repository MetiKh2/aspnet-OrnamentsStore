using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Dtos.Ornament
{
    public class EditOrnamentDto: CreateOrnamentDto
    {
        public long  Id { get; set; }
    }
}
