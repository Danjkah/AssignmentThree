using Microsoft.EntityFrameworkCore;
using AssignmentThree.Core.Interfaces;
using AssignmentThree.Core.Models;
using AssignmentThree.Infrastructure.Data;
using System.Data.Common;
using SQLitePCL;

namespace AssignmentThree.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AssignmentDbContext _context;

    public CommentRepository(AssignmentDbContext context)
    {
        _context = context; 
    }

    public async Task<bool> CommentExistsAsync(int id)
    {
        return await _context.Comments.AnyAsync(c => c.Id == id);
    }

    public async Task<Comment> CreateCommentAsync(int postId, Comment comment)
    {
        comment.CreatedDate = DateTime.UtcNow;
        comment.PostId = postId; 
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return comment; 



    }

    public async Task<bool> DeleteCommentAsync(int id)
    {
        var commentToDelete = await _context.Comments.FindAsync(id);
        if (commentToDelete == null)
        {
            return false;
        }
        _context.Comments.Remove(commentToDelete);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(int postId)
    {
        return await _context.Comments
                       .Where(c => c.PostId == postId)
                       .ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment?> UpdateCommentAsync(Comment comment)
    {
        var existingComment = await _context.Comments.FindAsync(comment.Id);

        if(existingComment == null)
        {
            return null;
        }
        existingComment.Email = comment.Email;
        existingComment.Name = comment.Name;
        existingComment.Content = comment.Content; 

        await _context.SaveChangesAsync();

        return existingComment;


    }

    
}