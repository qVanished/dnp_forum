using System;

namespace ApiContracts;

public class UpdatePostDTO
{
    public string Title { get; set; }
    public string Body { get; set; }

    public UpdatePostDTO(string title, string body)
    {
        Title = title;
        Body = body;
    
    }
}