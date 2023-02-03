using Ornaments.Core.Dtos.Ornament.Category;
using Ornaments.Core.Dtos.Paging;

namespace Ornaments.Core.Dtos.Ornament
{
    public class FilterOrnamentDto:BasePaging
    {
        public List<DataAccess.Entities.Ornaments.Ornament> Ornaments{ get; set; }
        public string Name { get; set; } = null;
        public long CategoryId { get; set; } = 0;
        public bool IsSlider { get; set; } = false;
        public bool IsMainPage { get; set; } = false;
        public bool IsDisplay { get; set; } = false;
        public bool MostViews { get; set; }

        #region methods
        public FilterOrnamentDto SetOrnaments(List<DataAccess.Entities.Ornaments.Ornament> ornaments)
        {
            this.Ornaments = ornaments;
            return this;
        }
        public FilterOrnamentDto SetPaging(BasePaging paging)
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
