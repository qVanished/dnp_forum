using System;
using System.Text.Json;
using ClassLibrary1;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comment.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<Comment>> readComments()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
    }

    private async Task writeComments(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await readComments();
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        await writeComments(comments);
        return comment;

    }


    public async Task DeleteAsync(int id)
    {
        List<Comment> comments = await readComments();
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID'{id}' not found");

        }
        comments.Remove(commentToRemove);
        await writeComments(comments);
    }

    public async Task<IQueryable<Comment>> GetManyAsync()
    {
        List<Comment> comments = await readComments();
        return comments.AsQueryable();
    }

    public async Task<IQueryable<Comment>> GetManyAsync(int postId)
    {
        List<Comment> comments = await readComments();
        List<Comment> filteredComment = new List<Comment>();
        foreach (Comment comment in comments)
        {
            if (comment.PostId == postId) filteredComment.Add(comment);

        }
        return filteredComment.AsQueryable();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await readComments();
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID'{id}' not found");
        }
        return existingComment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await readComments();
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with Id '{comment.Id}' not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        await writeComments(comments);
        
    }
}
