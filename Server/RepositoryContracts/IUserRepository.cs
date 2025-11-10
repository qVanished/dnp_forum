using System;
using ClassLibrary1;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    Task<User> GetSingleAsync(string username, string password);
    Task<IQueryable<User>> GetManyAsync();
}
