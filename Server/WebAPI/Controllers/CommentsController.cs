using ApiContracts;
using ClassLibrary1;
using FileRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }


        [HttpPost]
        public async Task<IResult> AddComment(

            [FromBody] CreateCommentDTO request)
        {
            Comment comment = new(request.Body, request.UserId, request.PostId);
            Comment created = await commentRepository.AddAsync(comment);
            return Results.Created($"/comments/{created.Id}", created);
        }

        [HttpPatch("{id:int}")]
        public async Task<IResult> UpdateComment(
            [FromRoute] int id,
            [FromBody] UpdateCommentDTO request)
        {
            Comment commentToBeUpdated = await commentRepository.GetSingleAsync(id);
            commentToBeUpdated.Body = request.Body;
            await commentRepository.UpdateAsync(commentToBeUpdated);
            return Results.NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IResult> GetComment([FromRoute] int id)
        {
            Comment comment = await commentRepository.GetSingleAsync(id);
            return Results.Ok(comment);
        }

        [HttpGet]
        public async Task<IResult> GetComments(
            [FromQuery] int? postId = null)
        {
            IQueryable<Comment>? comments = null;
            if (postId is null)
            {
                comments = await commentRepository.GetManyAsync();
            }
            else
            {
                comments = await commentRepository.GetManyAsync((int)postId);
            }

            return Results.Ok(comments);
        }

        [HttpDelete("{id:int}")]
        public async Task<IResult> DeleteComment([FromRoute] int id)
        {
            await commentRepository.DeleteAsync(id);
            return Results.NoContent();

        }
        
    } 

}