using System;
using RepositoryContracts;
using ClassLibrary1;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    private async Task AddCommentAsync(string body, int userId, int postId)
    {
        Comment created = await commentRepository.AddAsync(new Comment(body, userId, postId));
        Console.WriteLine($"Comment successfully created: {created}");

    }

    public async Task StartAsync()
    {

        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Input body:");
        string body = Console.ReadLine();
        Console.WriteLine("Input user ID:");
        int userId = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Input post ID:");
        int postId = Int32.Parse(Console.ReadLine());

        await AddCommentAsync(body, userId, postId);
       
    }
}
