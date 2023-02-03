using RadioMeti.Application.DTOs.Admin.Category.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioMeti.Application.DTOs.Admin.Category.Edit
{
    public class EditCategoryDto:CreateCategoryDto
    {
        public long Id { get; set; }
    }
}
