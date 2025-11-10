using System;

namespace ApiContracts;

public class CreateCommentDTO
{
    public string Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }



    public CreateCommentDTO(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}
