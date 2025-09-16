using System;
using System.Text.Json;
using ClassLibrary1;
using RepositoryContracts;


namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "post.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<Post>> readPosts()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
    }

    private async Task writePosts(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }
    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await readPosts();
        post.Id = posts.Any()
        ? posts.Max(p => p.Id) + 1
        : 1;
        posts.Add(post);
        await writePosts(posts);
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await readPosts();
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID'{id}' not found");

        }
        posts.Remove(postToRemove);
        await writePosts(posts);
    }

    public async Task<IQueryable<Post>> GetManyAsync()
    {
        List<Post> posts = await readPosts();
        return posts.AsQueryable();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await readPosts();
        Post? existingPost = posts.SingleOrDefault(p => p.Id == id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID'{id}' not found");
        }
        return existingPost;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await readPosts();
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with Id '{post.Id}' not found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        await writePosts(posts);

    }
}
