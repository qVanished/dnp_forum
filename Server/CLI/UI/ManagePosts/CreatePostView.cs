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
        Console.WriteLine($"Post successfully created: {created.Id}");

    }
    public async Task StartAsync()
    {

        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Input title:");
        string title = Console.ReadLine();
        Console.WriteLine("Input body:");
        string body = Console.ReadLine();
        Console.WriteLine("Input user ID:");
        int userId = Int32.Parse(Console.ReadLine());

        await AddPostAsync(title, body, userId);
    }
}
