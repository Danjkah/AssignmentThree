using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AssignmentThree.Core.DTOs;
using AssignmentThree.Core.Interfaces;
using AssignmentThree.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class postsController : ControllerBase
{
    private readonly IPostRepository _postRepository;

 public postsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    //GET: api/post
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await _postRepository.GetAllPostsAsync();
        return Ok(posts);
    }

    //GET: api/posts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);

    }

    //POST: api/posts
    [HttpPost]
    public async Task<ActionResult<Task>> CreatePost([FromBody] PostCreateDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); //this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }

        var post = new Post
        {
          Title = postDto.Title,
          Content = postDto.Content,
        
        };

        var createdPost = await _postRepository.CreatePostAsync(post);

        return CreatedAtAction(
            nameof(GetPost),
            new {id = createdPost.Id},
            createdPost
        );
    }

    //PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] PostUpdateDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);//this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }

        var exists = await _postRepository.PostExistsAsync(id);
        if (!exists)
        {
            return NotFound();
        }

        var post = new Post
        {
            Id = id,
            Title = postDto.Title,
            Content = postDto.Content,
        };

        var UpdatedPost = await _postRepository.UpdatePostAsync(post);

        return Ok(UpdatedPost);
    }
    
    //PATCH: api/posts/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest (new {message = "Patch document is null"});//this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(post, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);//this is not in the requirements of the assignment but I added it in because it just makes sense to have
        }
        post.UpdatedDate = DateTime.UtcNow;

        await _postRepository.UpdatePostAsync(post);

        return Ok(post);

    }
    
    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var deleted = await _postRepository.DeletePostAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }



}