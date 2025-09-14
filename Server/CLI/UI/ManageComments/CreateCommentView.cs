using System;
using InMemoryRepositories;
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
}
