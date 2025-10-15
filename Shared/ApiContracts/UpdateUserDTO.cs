using System;

namespace ApiContracts;

public class UpdateUserDTO
{
    public string Username { get; set; }
    public string Password { get; set; }

    public UpdateUserDTO(string username, string password)
    {
        Username = username;
        Password = password;
      
    }
}
