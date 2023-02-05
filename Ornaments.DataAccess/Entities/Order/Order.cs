using Ornaments.DataAccess.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.DataAccess.Entities.Order
{
    public class Order:BaseEntity
    {
        #region props
        public string UserId { get; set; }
        public DateTime? PermentDate { get; set; }
        public bool IsPay { get; set; }
        [Display(Name = "کد پیگیری")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TrackingCode { get; set; }
        [Display(Name = "شرح ادمین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AdminDescription { get; set; }
        [Display(Name = "شرح ادمین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        #endregion
        #region rel
        public ICollection<OrderDetail> OrderDetails { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User{ get; set; }
        #endregion
    }
}
