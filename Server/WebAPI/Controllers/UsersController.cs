using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
namespace WebAPI.Controllers;
using ApiContracts;
using ClassLibrary1;

    
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }


    [HttpPost]
    public async Task<IResult> AddUser(

        [FromBody] CreateUserDTO request,
        [FromServices] IUserRepository userRepository)
    {
        User user = new(request.Username, request.Password);
        User created = await userRepository.AddAsync(user);
        return Results.Created($"/users/{created.Id}", created);
    }

    [HttpPatch("{id:int}")]
    public async Task<IResult> UpdateUser(
        [FromRoute] int id,
        [FromBody] UpdateUserDTO request)
    {
        User userToBeUpdated = await userRepository.GetSingleAsync(id);
        userToBeUpdated.Username = request.Username;
        userToBeUpdated.Password = request.Password;
        await userRepository.UpdateAsync(userToBeUpdated);
        return Results.NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetUser([FromRoute] int id)
    {
        User user = await userRepository.GetSingleAsync(id);
        return Results.Ok(user);
    }

    [HttpGet]
    public async Task<IResult> GetUsers(
        [FromQuery] int? user = null)
    {
        IQueryable<User>? users = null;

        users = await userRepository.GetManyAsync();


        return Results.Ok(users);
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteUser([FromRoute] int id)
    {
        await userRepository.DeleteAsync(id);
        return Results.NoContent();

    }

}
    

