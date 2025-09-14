using System;
using ClassLibrary1;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{

    List<Comment> comments;
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
        ? comments.Max(p => p.Id) + 1
        : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID'{id}' not found");

        }
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<IQueryable<Comment>> GetManyAsync()
    {
        return Task.FromResult(comments.AsQueryable());
    }

    public Task<IQueryable<Comment>> GetManyAsync(int id)
    {
        List<Comment> filteredComment = new List<Comment>();
        foreach (Comment comment in comments)
        {
            if (comment.PostId == id) filteredComment.Add(comment);

        }
        
        return Task.FromResult(filteredComment.AsQueryable());
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID'{id}' not found");
        }
        return Task.FromResult(existingComment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with Id '{comment.Id}' not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

   
}

