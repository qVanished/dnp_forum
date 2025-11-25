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
        User? existing = await ctx.Posts.SingleOrDefaultAsync(u => u.Id == id); 
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

    public Task<User> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetSingleAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

     public async Task UpdateAsync(User user) 
    { 
        if (!await ctx.Users.AnyAsync(u => u.Id == user.Id)) 
        { 
            throw new InvalidOperationException($"User with id {user.Id} not found"); 
        } 
        ctx.Users.Update(user); 
        await ctx.SaveChangesAsync(); 
    }
    }
