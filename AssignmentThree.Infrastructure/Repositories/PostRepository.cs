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


    public async Task<bool> UpdatePostAsync(int id, Post post)
    {

        var postToUpdate = await _context.Posts.FindAsync(id);
        if (postToUpdate == null)
        {
            return false;
        }
        postToUpdate.Author = post.Author;
        postToUpdate.Content = post.Content;
        postToUpdate.UpdatedDate = DateTime.UtcNow;
        postToUpdate.Title = post.Title;

        await _context.SaveChangesAsync();
        return  true; 

    }
}