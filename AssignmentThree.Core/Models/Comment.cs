using System.ComponentModel.DataAnnotations;

namespace AssignmentThree.Core.Models;


public class Comment
{
    public int Id {get;set;}

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

    public DateTime CreatedDate {get;set;} = DateTime.UtcNow;

    public required Post Post {get;set;} 
}
