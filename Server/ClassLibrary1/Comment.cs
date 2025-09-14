using System;

namespace ClassLibrary1;

public class Comment
{
    public string Body { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Comment(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}
