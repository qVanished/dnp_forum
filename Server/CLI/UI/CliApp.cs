using System;
using ClassLibrary1;
using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        UserRepository = userRepository;
        CommentRepository = commentRepository;
        PostRepository = postRepository;
    }

    public IUserRepository UserRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public IPostRepository PostRepository { get; }

    internal async Task StartAsync()
    {
        while (true)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("cu - Create user");
            Console.WriteLine("pt - Create post");
            Console.WriteLine("cc - Create comment");
            Console.WriteLine("vp - View post");
            Console.WriteLine("vps - View posts");

            string value = Console.ReadLine();

            var view = new Object();
            switch (value)
            {
                case "cu":
                    view = new CreateUserView(UserRepository);
                    break;

                case "pt":
                    view = new CreatePostView(PostRepository);
                    break;

                case "cc":
                    view = new CreateCommentView(CommentRepository);
                    break;

                case "vp":
                    view = new SinglePostView(PostRepository, CommentRepository);
                    break;

                case "vps":
                    view = new ManagePostsView(PostRepository);
                    break;

                default:
                    Console.WriteLine("Action not supported");
                    break;
            }
        }
        //add start Async methods to views.

    }
}
