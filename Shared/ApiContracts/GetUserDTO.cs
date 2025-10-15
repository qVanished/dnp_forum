using System;

namespace ApiContracts;

public class GetUserDTO
{
    public string Username { get; set; }
    public int Id { get; set; }
    public GetUserDTO(string username, int id)
    {
        Username = username;
        Id = id;
    }
}