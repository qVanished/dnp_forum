using System;
using ClassLibrary1;
using RepositoryContracts;

namespace EfcRepositories;

public class EFCCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;
    public EFCCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment) 
    { 
        await ctx.Comments.AddAsync(comment); 
        await ctx.SaveChangesAsync(); 
        return comment; 
    }

    public async Task DeleteAsync(int id) 
    { 
        Comment? existing = ctx.Comments.SingleOrDefault(p => p.Id == id); 
        if (existing == null) 
        { 
            throw new InvalidOperationException($"Comment with id {id} not found"); 
        } 
        ctx.Comments.Remove(existing); 
        await ctx.SaveChangesAsync(); 
    }


    public Task<IQueryable<Comment>> GetManyAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<Comment>> GetManyAsync()
    {
        return ctx.Comments.AsQueryable<Comment>(); 
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        return ctx.Comments.First(c => c.Id == id);
    }

    public async Task UpdateAsync(Comment comment) 
{ 
  if (!ctx.Comments.Any(c => c.Id == comment.Id)) 
  { 
    throw new InvalidOperationException($"Comment with id {comment.Id} not found"); 
  } 
  ctx.Comments.Update(comment); 
  await ctx.SaveChangesAsync(); 
}
}
