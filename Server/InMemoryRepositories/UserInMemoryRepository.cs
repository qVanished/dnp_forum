using System;
using ClassLibrary1;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    List<User> users = new List<User>();
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
        ? users.Max(p => p.Id) + 1
        : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task DeleteAsync(int id)
    {
        
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID'{id}' not found");

        }
        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<IQueryable<User>> GetManyAsync()
    {
        return Task.FromResult(users.AsQueryable());
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID'{id}' not found");
        }
        return Task.FromResult(existingUser);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with Id '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }
}
