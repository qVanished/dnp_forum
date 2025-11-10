using ApiContracts;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {

        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IResult> Login(
            [FromBody] CreateUserDTO request
        )
        {
            User? user = await userRepository.GetSingleAsync(request.Username, request.Password);

            if (user is null)
            {
                return Results.Unauthorized();
            }

            GetUserDTO userDTO = new GetUserDTO(user.Username, user.Id);

            return Results.Ok(userDTO);
        }
    }
}
