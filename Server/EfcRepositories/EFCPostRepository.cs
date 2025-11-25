using System;
using ClassLibrary1;
using RepositoryContracts;

namespace EfcRepositories;

public class EFCPostRepository : IPostRepository
{
    private readonly AppContext ctx;
    public EFCPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<Post> AddAsync(Post post) 
    { 
        await ctx.Posts.AddAsync(post); 
        await ctx.SaveChangesAsync(); 
        return post; 
    }

    public async Task DeleteAsync(int id) 
    { 
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new InvalidOperationException($"Post with id {id} not found"); 
        } 
        ctx.Posts.Remove(existing); 
        await ctx.SaveChangesAsync(); 
    }

    public async Task<IQueryable<Post>> GetManyAsync()
    {
        return ctx.Posts.AsQueryable(); 

    }

    public Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Post post) 
    { 
        if (!await ctx.Posts.AnyAsync(p => p.Id == post.Id)) 
        { 
            throw new InvalidOperationException($"Post with id {post.Id} not found"); 
        } 
        ctx.Posts.Update(post); 
        await ctx.SaveChangesAsync(); 
    }
}
