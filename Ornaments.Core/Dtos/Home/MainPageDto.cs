

using Ornaments.DataAccess.Entities.Ornaments;

namespace Ornaments.Core.Dtos.Home
{
    public class MainPageDto
    {
        public List<DataAccess.Entities.Ornaments.Ornament> SliderOrnaments{ get; set; }
        public List<DataAccess.Entities.Ornaments.Ornament> MainOrnaments { get; set; }
        public List<DataAccess.Entities.Ornaments.Ornament> MostViewsOrnaments { get; set; }
	}
}
