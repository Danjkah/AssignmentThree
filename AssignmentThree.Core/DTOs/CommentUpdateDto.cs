using System.ComponentModel.DataAnnotations;

namespace AssignmentThree.Core.DTOs;


public class CommentUpdateDto
{
    [Required]
    public int PostId {get;set;}

    [Required]
    [MaxLength(100)]
    public string Name {get;set;} = string.Empty;
    
    [Required]
    [MaxLength(150)]
    public string Email {get;set;} = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Content {get;set;} = string.Empty;
}
