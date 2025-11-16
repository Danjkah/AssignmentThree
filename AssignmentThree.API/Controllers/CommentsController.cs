using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AssignmentThree.Core.DTOs;
using AssignmentThree.Core.Interfaces;
using AssignmentThree.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class commentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

 public commentsController(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
    }

    //GET: api/Comment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
    {
     var comments = await _commentRepository.GetAllCommentsAsync();
     return Ok(comments);

    }

    //GET: api/comments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);

    }

    //PUT: api/comments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);//this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }

        var exists = await _commentRepository.CommentExistsAsync(id);
        if (!exists)
        {
            return NotFound();
        }



        var post = new Comment
        {
            Id = id,
            Name = updateDto.Name,
            Email = updateDto.Email,
            Content = updateDto.Content,
        };

        var UpdatedPost = await _commentRepository.UpdateCommentAsync(post);

        return Ok(UpdatedPost);
    }

      
    //PATCH: api/posts/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest (new {message = "Patch document is null"});//this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }
        var comment = await _commentRepository.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(comment, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);//this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }
        

        await _commentRepository.UpdateCommentAsync(comment);

        return Ok(comment);

    }

    //PATCH: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var deleted = await _commentRepository.DeleteCommentAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }



    // GET: api/posts/{postId}/comments
    [HttpGet("/api/posts/{postId}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsForPost(int postId)
    {
        var comments = await _commentRepository.GetAllCommentsByPostIdAsync(postId);

        return Ok(comments); 
    }

    //POST: api/posts/{postId}/comments
    [HttpPost("/api/posts/{postId}/comments")]
    public async Task<ActionResult<Task>> CreateComment(int postId, [FromBody] CommentCreateDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); //this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }

        var comment = new Comment
        {
          Name = commentDto.Name,
          Email = commentDto.Email,
          Content = commentDto.Content,
          PostId = postId,
        
        };

        var createdComment = await _commentRepository.CreateCommentAsync(postId, comment);

        return CreatedAtAction(
            nameof(GetComment),
            new {id = createdComment.Id},
            createdComment
        );
    }




}