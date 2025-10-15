using System;

namespace ApiContracts;

public class UpdateCommentDTO
{
    public required string Body { get; set; }

    public UpdateCommentDTO(string body)
    {
        Body = body;
    }

}
