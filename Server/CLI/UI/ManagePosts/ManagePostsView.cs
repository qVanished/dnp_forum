using System;
using System.Collections.Generic;
using ClassLibrary1;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;

    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }


    private async Task displayPostAsync()
    {
        var values = await postRepository.GetManyAsync();
        foreach (Post post in values)
        {
            Console.WriteLine($"Post: {post.Id} {post.Title} ");
        }

    }

    public async Task StartAsync()
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        await displayPostAsync();
    }
}
