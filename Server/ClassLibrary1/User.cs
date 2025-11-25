using System;

namespace ClassLibrary1;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }
    public List<Post> Posts {get; set;} 
    public List<Comment> Comments {get; set;}

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        
    }

    private User(){}
}
