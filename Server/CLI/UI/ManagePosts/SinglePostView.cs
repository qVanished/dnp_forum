using System;
using ClassLibrary1;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    private async Task displayPostAsync(int id)
    {
        Post post = await postRepository.GetSingleAsync(id);
        Console.WriteLine($"Post: {post.Id} {post.Title} \n{post.Body} ");
        var comments = await commentRepository.GetManyAsync(id);
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"Comment: {comment}");
        }
    }

    public async Task StartAsync()
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine("Input ID:");
        int id = Int32.Parse(Console.ReadLine());

        await displayPostAsync(id);
    }
}
