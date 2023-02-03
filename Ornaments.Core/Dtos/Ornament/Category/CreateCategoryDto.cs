 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioMeti.Application.DTOs.Admin.Category.Create
{
    public class CreateCategoryDto
    {
        [MaxLength(200)]
        [Required]
        public string CategoryName { get; set; }
    }
}
