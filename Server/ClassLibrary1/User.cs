using System;

namespace ClassLibrary1;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }

    private static int ID = 0;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Id = ID++;
    }
}
