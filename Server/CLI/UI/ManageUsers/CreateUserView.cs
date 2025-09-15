using System;
using RepositoryContracts;
using ClassLibrary1; 

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    private async Task AddUserAsync(string username, string password)
    {
        User created = await userRepository.AddAsync(new User(username, password));
        Console.WriteLine($"User successfully created: {created}");

    }
    public async Task StartAsync()
    {

        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Input username:");
        string? username = Console.ReadLine();
        Console.WriteLine("Input password:");
        string? password = Console.ReadLine();

        await AddUserAsync(username, password);
    }

}
