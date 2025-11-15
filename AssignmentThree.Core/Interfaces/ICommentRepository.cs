using AssignmentThree.Core.Models;

namespace AssignmentThree.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetAllCommentsForPostAsync(int postId);
        Task<Comment> CreateCommentAsync(int postId);
        Task<bool> UpdateCommentAsync(int id, Comment comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
