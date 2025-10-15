using System;

namespace ApiContracts;

public class CreateUserDTO
{
    public string Username { get; set; }
    public string Password { get; set; }

    public CreateUserDTO(string username, string password)
    {
        Username = username;
        Password = password;
      
    }
}