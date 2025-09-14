using System;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;
     public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
}

