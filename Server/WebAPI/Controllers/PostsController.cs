using ApiContracts;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }


        [HttpPost]
        public async Task<IResult> AddPost(

            [FromBody] CreatePostDTO request,
            [FromServices] IUserRepository userRepository)
        {
            Post post = new( request.Title, request.Body, request.UserId);
            Post created = await postRepository.AddAsync(post);
            return Results.Created($"/posts/{created.Id}", created);
        }

        [HttpPatch("{id:int}")]
        public async Task<IResult> UpdatePost(
            [FromRoute] int id,
            [FromBody] UpdatePostDTO request)
        {
            Post postToBeUpdated = await postRepository.GetSingleAsync(id);
            postToBeUpdated.Title = request.Title;
            postToBeUpdated.Body = request.Body;
            await postRepository.UpdateAsync(postToBeUpdated);
            return Results.NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IResult> GetPost([FromRoute] int id)
        {
            Post post = await postRepository.GetSingleAsync(id);
            return Results.Ok(post);
        }

        [HttpGet]
        public async Task<IResult> GetPosts(
            [FromQuery] int? userId = null,
            [FromQuery] string? title = null)
        {
            List<Post>? postsToBeSent = new List<Post>();
           
            var posts = await postRepository.GetManyAsync();

            foreach (var post in posts)
            {
                if (userId is not null)
                {
                    if (!string.IsNullOrEmpty(title))
                    {
                        if (post.Title.Contains(title) && userId == post.UserId)
                        {
                            postsToBeSent.Add(post);
                        }
                    }
                    else if (userId == post.UserId)
                    {
                        postsToBeSent.Add(post);
                    }
                }
                else if (!string.IsNullOrEmpty(title))
                {
                    if (post.Title.Contains(title))
                    {
                        postsToBeSent.Add(post);
                    }
                }
                else
                {
                    postsToBeSent.Add(post);
                }
            }

            //postsToBeSent = (List<Post>)(from post in posts where userId == post.Id && post.Title.Contains(title) select post);

            return Results.Ok(postsToBeSent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IResult> DeleteComment([FromRoute] int id)
        {
            await postRepository.DeleteAsync(id);
            return Results.NoContent();

        }

    }
}