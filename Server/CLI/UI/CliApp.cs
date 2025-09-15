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

           
            switch (value)
            {
                case "cu":
                    var view = new CreateUserView(UserRepository);
                    await view.StartAsync();
                    break;

                case "pt":
                    var view1 = new CreatePostView(PostRepository);
                    await view1.StartAsync();
                    break;

                case "cc":
                    var view2 = new CreateCommentView(CommentRepository);
                    await view2.StartAsync();
                    break;

                case "vp":
                    var view3 = new SinglePostView(PostRepository, CommentRepository);
                    await view3.StartAsync();
                    break;

                case "vps":
                    var view4 = new ManagePostsView(PostRepository);
                    await view4.StartAsync();
                    break;

                default:
                    Console.WriteLine("Action not supported");
                    break;
            }
            
        }

    }
}
