

using Microsoft.AspNetCore.Identity;
using Ornaments.Core.Dtos;
using Ornaments.Core.Dtos.Ornament;
using Ornaments.Core.Dtos.Ornament.Category;
using Ornaments.DataAccess.Entities.Ornaments;
using RadioMeti.Application.DTOs.Admin.Category.Create;
using RadioMeti.Application.DTOs.Admin.Category.Edit;

namespace Ornaments.Core.Services.Interfaces
{
	public interface IOrnamentService
	{
        Task<FilterCategoryDto> FilterCategories(FilterCategoryDto filter);
        Task<string> CreateCategory(CreateCategoryDto create);
        Task<string> EditCategory(EditCategoryDto edit);
        Task<string> DeleteCategory(long id);
        Task<Category> GetCategory(long id);
        Task<List<Category>> GetCategories();


        Task<FilterOrnamentDto> FilterOrnaments(FilterOrnamentDto filter);
        Task<string> CreateOrnament(CreateOrnamentDto create);
        Task<string> EditOrnament(EditOrnamentDto edit);
        Task<string> DeleteOrnament(long id);
        Task<Ornament> GetOrnament(long id);

    }
}
