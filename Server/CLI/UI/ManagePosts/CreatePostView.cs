using System;
using RepositoryContracts;
using ClassLibrary1;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

     private async Task AddPostAsync(string title, string body, int userId)
    {
        Post created = await postRepository.AddAsync(new Post(title, body, userId));
        Console.WriteLine($"Post successfully created: {created}");

    }
}
