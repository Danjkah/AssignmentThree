using AssignmentThree.Core.Models;

namespace AssignmentThree.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(Post post);
         Task<bool> UpdatePostAsync(int id, Post post);
        Task<bool> PatchPostAsync(int id, Post partialPost);
        Task<bool> DeletePostAsync(int id);
    }
}
