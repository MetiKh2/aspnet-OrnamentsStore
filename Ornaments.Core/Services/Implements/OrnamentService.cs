using Microsoft.EntityFrameworkCore;
using Ornaments.Core.Dtos;
using Ornaments.Core.Dtos.Ornament;
using Ornaments.Core.Dtos.Ornament.Category;
using Ornaments.Core.Services.Interfaces;
using Ornaments.DataAccess.Entities.Ornaments;
using Ornaments.DataAccess.Repository;
using RadioMeti.Application.DTOs.Admin.Category.Create;
using RadioMeti.Application.DTOs.Admin.Category.Edit;
using Ornaments.Core.Dtos.Paging;

namespace Ornaments.Core.Services.Implements
{
    public class OrnamentService : IOrnamentService
    {
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Ornament> _ornamentRepository;

        public OrnamentService(IGenericRepository<Category> categoryRepository, IGenericRepository<Ornament> ornamentRepository)
        {
            _categoryRepository = categoryRepository;
            _ornamentRepository = ornamentRepository;
        }

        public async Task<string> CreateCategory(CreateCategoryDto create)
        {
            try
            {
                var item = new Category
                {
                    CategoryName = create.CategoryName
                };
                await _categoryRepository.AddEntity(item);
                await _categoryRepository.SaveChangesAsync();
                return "Category Successfully added";

            }
            catch (Exception e)
            {
                return "Something is wrong";
                throw;
            }
        }

        public async Task<string> CreateOrnament(CreateOrnamentDto create)
        {
            try
            {
                var item = new Ornament
                {
                    Brand = create.Brand,
                    Name = create.Name,
                    CategoryId = create.CategoryId,
                    Description = create.Description,
                    Image = create.Image,
                    Invertory = create.Invertory,
                    IsDisplay = create.IsDisplay,
                    IsInMainPage = create.IsInMainPage,
                    IsSlider = create.IsSlider,
                    Price = create.Price,
                };
                await _ornamentRepository.AddEntity(item);
                await _ornamentRepository.SaveChangesAsync();
                return "Ornament Successfully added";

            }
            catch (Exception e)
            {
                return "Something is wrong";
                throw;
            }
        }

        public async Task<string> DeleteCategory(long id)
        {
            try
            {
                var item = await _categoryRepository.GetEntityById(id);
                if (item == null) return null;
                _categoryRepository.DeleteEntity(item);
                await _categoryRepository.SaveChangesAsync();

                return "Category Successfully deleted";

            }
            catch (Exception e)
            {
                return "Something is wrong";
                throw;
            }
        }

        public async Task<string> DeleteOrnament(long id)
        {
            try
            {
                var item = await _ornamentRepository.GetEntityById(id);
                if (item == null) return null;
                _ornamentRepository.DeleteEntity(item);
                await _ornamentRepository.SaveChangesAsync();

                return "Ornament Successfully deleted";

            }
            catch (Exception e)
            {
                return "Something is wrong";
                throw;
            }
        }

        public async Task<string> EditCategory(EditCategoryDto edit)
        {
            try
            {
                var item = await _categoryRepository.GetEntityById(edit.Id);
                if (item == null) return null;
                item.CategoryName = edit.CategoryName;
                _categoryRepository.EditEntity(item);
                await _categoryRepository.SaveChangesAsync();
                return "Category Successfully edited";
            }
            catch
            {
                return "Something is wrong";

            }
        }

        public async Task<string> EditOrnament(EditOrnamentDto edit)
        {
            try
            {
                var item = await _ornamentRepository.GetEntityById(edit.Id);
                if (item == null) return null;
                item.Brand = edit.Brand;
                   item.Name = edit.Name;
                  item.CategoryId = edit.CategoryId;
                  item.Description = edit.Description;
                  item.Image = edit.Image;
                  item.Invertory = edit.Invertory;
                  item.IsDisplay = edit.IsDisplay;
                 item.IsInMainPage = edit.IsInMainPage;
                  item.IsSlider = edit.IsSlider;
                  item.Price = edit.Price;
                _ornamentRepository.EditEntity(item);
                await _categoryRepository.SaveChangesAsync();
                return "Ornament Successfully edited";
            }
            catch
            {
                return "Something is wrong";

            }
        }

        public async Task<FilterCategoryDto> FilterCategories(FilterCategoryDto filter)
        {
            var query = _categoryRepository.GetQuery();

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var all = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetCategories(all).SetPaging(pager);
        }

        public async Task<FilterOrnamentDto> FilterOrnaments(FilterOrnamentDto filter)
        {
            var query = _ornamentRepository.GetQuery().Include(p=>p.Category).AsQueryable();
            #region filter
            if (!string.IsNullOrEmpty(filter.Name)) query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower())).AsQueryable();
            if(filter.IsDisplay) query = query.Where(p => p.IsDisplay).AsQueryable();
            if(filter.IsMainPage) query = query.Where(p => p.IsInMainPage).AsQueryable();
            if(filter.IsSlider) query = query.Where(p => p.IsSlider).AsQueryable();
            if(filter.CategoryId != null&&filter.CategoryId>0) query = query.Where(p => p.CategoryId==filter.CategoryId).AsQueryable();
            #endregion
            #region order
            if (filter.MostViews) query = query.OrderByDescending(p => p.ViewCount).AsQueryable();
            else query = query.OrderByDescending(p => p.CreateDate).AsQueryable();
            #endregion
            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var all= await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetOrnaments(all).SetPaging(pager);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetQuery().ToListAsync();
        }

        public async Task<Category> GetCategory(long id)
        {
            return await _categoryRepository.GetEntityById(id);
        }

        public async Task<Ornament> GetOrnament(long id)
        {
            return await _ornamentRepository.GetEntityById(id);

        }
    }
}
