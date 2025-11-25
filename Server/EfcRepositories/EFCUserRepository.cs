using System;
using ClassLibrary1;
using RepositoryContracts;

namespace EfcRepositories;

public class EFCUserRepository : IUserRepository
{
    private readonly AppContext ctx;
    public EFCUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user) 
    { 
        await ctx.Users.AddAsync(user); 
        await ctx.SaveChangesAsync(); 
        return user; 
    }

    public async Task DeleteAsync(int id) 
    { 
        User? existing = ctx.Users.SingleOrDefault(u => u.Id == id); 
        if (existing == null) 
        { 
            throw new InvalidOperationException($"User with id {id} not found"); 
        } 
        ctx.Users.Remove(existing); 
        await ctx.SaveChangesAsync(); 
    }

    public async Task<IQueryable<User>> GetManyAsync()
    {
        return ctx.Users.AsQueryable(); 
    }

    public async Task<User> GetSingleAsync(int id)
    {
        return ctx.Users.First(u => u.Id == id);
    }

    public async Task<User> GetSingleAsync(string username, string password)
    {
        return ctx.Users.First(u => u.Username.Equals(username) && u.Password.Equals(password));
    }

     public async Task UpdateAsync(User user) 
    { 
        if (!ctx.Users.Any(u => u.Id == user.Id)) 
        { 
            throw new InvalidOperationException($"User with id {user.Id} not found"); 
        } 
        ctx.Users.Update(user); 
        await ctx.SaveChangesAsync(); 
    }
    }
