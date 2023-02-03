using Microsoft.AspNetCore.Identity;
using Ornaments.Core.Dtos.Admin.Users;
using Ornaments.Core.Dtos.Admin.Users.Create;
using Ornaments.Core.Dtos.Admin.Users.Edit;
using Ornaments.DataAccess.Entities.User;
using System.Security.Claims;

namespace Ornaments.Core.Services.Interfaces;

public interface IUserService
{
    Task<FilterUsersDto> FilterUsersList(FilterUsersDto filter);
    Task<string> CreateUser(CreateUserDto create);
    Task<ApplicationUser> GetById(string id);
    Task<ApplicationUser> GetByName(string userName);
    Task<string?> EditUser(EditUserDto edit);
    Task<string?> DeleteUser(string userName);
}