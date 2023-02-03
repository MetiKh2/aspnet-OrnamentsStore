using Ornaments.Core.Dtos.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Dtos.Ornament.Category
{
    public class FilterCategoryDto:BasePaging
    {
        public List<DataAccess.Entities.Ornaments.Category> Categories { get; set; }
        #region methods
        public FilterCategoryDto SetCategories(List<DataAccess.Entities.Ornaments.Category> categories)
        {
            this.Categories=categories;
            return this;
        }
        public FilterCategoryDto SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.EndPage = paging.EndPage;
            this.StartPage = paging.StartPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.SkipEntity = paging.SkipEntity;
            this.TakeEntity = paging.TakeEntity;
            this.PageCount = paging.PageCount;
            return this;
        }
        #endregion
    }
}
