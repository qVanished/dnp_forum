using System;
using ApiContracts;
using ClassLibrary1;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<CreatePostDTO> AddPostAsync(CreatePostDTO request);
    public Task UpdatePostAsync(int id, UpdatePostDTO request);
    public Task<Post> GetPostByIdAsync(int id);
    public Task<List<Post>> GetPostsAsync();
    public Task<List<Post>> GetPostsAsync(int? userId, string? title);
    public Task DeletePostAsync(int id);
}
