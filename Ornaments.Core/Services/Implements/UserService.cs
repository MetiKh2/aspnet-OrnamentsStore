using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ornaments.Core.Services.Interfaces;
using Ornaments.Core.Dtos.Paging;
using Ornaments.Core.Dtos.Admin.Users.Create;
using Ornaments.Core.Dtos.Admin.Users.Edit;
using Ornaments.Core.Dtos.Admin.Users;
using Ornaments.DataAccess.Entities.User;

namespace Ornaments.Core.Services.Implements;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> CreateUser(CreateUserDto create)
    {
        try
        {
            var user = new ApplicationUser()
            {
                Email = create.Email,
                UserName = create.UserName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, create.Password);
            return "User Created Successfully";
        }
        catch (Exception)
        {
            return "Something is wrong ";
            throw;
        }
    }

    public async Task<string?> DeleteUser(string userName)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return null;
            await _userManager.DeleteAsync(user);
            return "User Deleted Successfully";
        }
        catch (Exception)
        {
            return "Something is wrong ";
            throw;
        }
    }

    public async Task<string?> EditUser(EditUserDto edit)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(edit.Id);
            if (user == null) return null;
            user.UserName = edit.UserName;
            user.Email = edit.Email;
            var result = await _userManager.UpdateAsync(user);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, token);
            return "User Edited Successfully";
        }
        catch (Exception)
        {
            return "Something is wrong ";
            throw;
        }
    }

    public async Task<FilterUsersDto> FilterUsersList(FilterUsersDto filter)
    {
        var query = _userManager.Users;
        #region state
        switch (filter.FilterUsersState)
        {
            case FilterUsersState.All:
                break;
            case FilterUsersState.EmailConfrimed:
                query = query.Where(p => p.EmailConfirmed).AsQueryable();
                break;
            case FilterUsersState.EmailNotConfrimed:
                query = query.Where(p => !p.EmailConfirmed).AsQueryable();
                break;
            default:
                break;
        }
        #endregion
        #region filter
        if (!string.IsNullOrEmpty(filter.UserName)) query = query.Where(p => p.UserName.Contains(filter.UserName)).AsQueryable();
        if (!string.IsNullOrEmpty(filter.Email)) query = query.Where(p => p.Email.Contains(filter.Email)).AsQueryable();
        #endregion
        #region paging
        var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
        var allUsers = await query.Paging(pager).Select(p => new UsersListDto { Email = p.Email, UserName = p.UserName }).ToListAsync();
        #endregion
        return filter.SetUsers(allUsers).SetPaging(pager);
    }

    public async Task<ApplicationUser> GetById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<ApplicationUser> GetByName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<IList<string>> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return await _userManager.GetRolesAsync(user);
    }


}
