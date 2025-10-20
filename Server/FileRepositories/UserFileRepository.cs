using System;
using System.Text.Json;
using ClassLibrary1;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "user.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<User>> readUsers()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
    }

    private async Task writeUsers(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
    public async Task<User> AddAsync(User user)
    {
        List<User> users = await readUsers();
        int maxId = users.Count > 0 ? users.Max(c => c.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        await writeUsers(users);
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await readUsers();
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID'{id}' not found");

        }
        users.Remove(userToRemove);
        await writeUsers(users);

    }


    public async Task<IQueryable<User>> GetManyAsync()
    {
        List<User> users = await readUsers();
        return users.AsQueryable();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await readUsers();
        User? existingUser = users.SingleOrDefault(p => p.Id == id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID'{id}' not found");
        }

        return existingUser;
    }


    public async Task UpdateAsync(User user)
    {
        List<User> users = await readUsers();
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with Id '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        await writeUsers(users);
    }
}
