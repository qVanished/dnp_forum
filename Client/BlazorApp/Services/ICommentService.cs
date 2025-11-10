using System;
using ApiContracts;
using ClassLibrary1;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CreateCommentDTO> AddCommentAsync(CreateCommentDTO request);
    public Task UpdateCommentAsync(int id, UpdateCommentDTO request);
    public Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    public Task<List<Comment>> GetCommentsAsync(int? userId, int? postId);
    public Task DeleteCommentAsync(int id);
}
