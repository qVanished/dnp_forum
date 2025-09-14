using System;

namespace ClassLibrary1;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    private static int ID = 0;

    public Post(string title, string body, int userId)
    {
        Id = ID++;
        Title = title;
        Body = body;
        UserId = userId;
    }
}
