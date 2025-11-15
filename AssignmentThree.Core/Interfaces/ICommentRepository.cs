using AssignmentThree.Core.Models;

namespace AssignmentThree.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAsync(int postId,  Comment comment);
        Task<Comment?> UpdateCommentAsync(Comment comment);

        Task<bool> CommentExistsAsync (int id);
        Task<bool> DeleteCommentAsync(int id);
    }
}
