using System;

namespace ApiContracts;

public class CreatePostDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }

     public CreatePostDTO(string title, string body, int userId)
    {
        
        Title = title;
        Body = body;
        UserId = userId;
    }
}
