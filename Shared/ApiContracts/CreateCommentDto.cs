using System;

namespace ApiContracts;

public class CreateCommentDTO
{
    public required string Body { get; set; }
    public required int UserId { get; set; }
    public required int PostId { get; set; }



    public CreateCommentDTO(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}
