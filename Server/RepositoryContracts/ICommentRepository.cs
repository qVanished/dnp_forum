using System;
using ClassLibrary1;

namespace RepositoryContracts;

public interface ICommentRepository
{
  Task<Comment> AddAsync(Comment comment);
  Task UpdateAsync(Comment comment);
  Task DeleteAsync(int id);
  Task<Comment> GetSingleAsync(int id);
  Task<IQueryable<Comment>> GetManyAsync();
  Task<IQueryable<Comment>> GetManyAsync(int id);
  
}
