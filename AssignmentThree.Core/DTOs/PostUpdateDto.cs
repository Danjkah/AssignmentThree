using System.ComponentModel.DataAnnotations;

namespace AssignmentThree.Core.Models;


public class PostUpdateDto
{    
    [Required]
    [MaxLength(200)]
    public string Title {get; set;} = string.Empty;

    [Required]
    public string Content {get; set;} = string.Empty;

    //public string Author {get; set;} = "admin"; //hardcoded


}