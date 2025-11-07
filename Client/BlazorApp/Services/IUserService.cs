using System;
using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<CreateUserDTO> AddUserAsync(CreateUserDTO request);
    public Task UpdateUserAsync(int id, UpdateUserDTO request);
    public Task<GetUserDTO> GetUserByIdAsync(int id);
    public Task<List<GetUserDTO>> GetUsersAsync();
    public Task DeleteUserAsync(int id);

}
