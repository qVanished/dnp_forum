using System;
using ClassLibrary1;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{

    List<Post> posts;
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
        ? posts.Max(p => p.Id) + 1
        : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetManyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with Id '{post.Id}' not found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }
}
