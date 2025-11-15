using AssignmentThree.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using AssignmentThree.Core.Models;
using AssignmentThree.Infrastructure.Data;
namespace AssignmentThree.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AssignmentDbContext _context;
    public PostRepository(AssignmentDbContext context)
    {
        _context = context;
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        post.CreatedDate = DateTime.UtcNow; 

        _context.Posts.Add(post);

        await _context.SaveChangesAsync();

        return post; 
        
    }

    public async Task<bool> DeletePostAsync(int id)
    {
       var postToDelete = await _context.Posts.FindAsync(id);
       if (postToDelete == null)
        {
            return false;
        }
        _context.Posts.Remove(postToDelete);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<bool> PostExistsAsync(int id)
    {
        return await _context.Posts.AnyAsync(p => p.Id == id);
    }

    public async Task<Post?> UpdatePostAsync(Post post)
    {

        var existingPost = await _context.Posts.FindAsync(post.Id);
        if (existingPost == null)
        {
            return null;
        }
        existingPost.Id = post.Id; 
        existingPost.Author = post.Author;
        existingPost.Content = post.Content;
        existingPost.UpdatedDate = DateTime.UtcNow;
        existingPost.Title = post.Title;

        await _context.SaveChangesAsync();
        return  existingPost; 

    }
}