using ClassLibrary1;
using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();
await userRepository.AddAsync(new User("dawid", "dupa123"));
await userRepository.AddAsync(new User("vane", "dupa444"));
await postRepository.AddAsync(new Post("Unaa", "qqq", 12));
await postRepository.AddAsync(new Post("Douaa", "nnn", 12));
await commentRepository.AddAsync(new Comment("nie", 123, 111));
await commentRepository.AddAsync(new Comment("nienie", 1232, 113));


CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
await cliApp.StartAsync();
