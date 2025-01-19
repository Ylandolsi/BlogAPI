using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public class Article
{
    [Key]
    public int IdArticle { get; set; }
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters"), MaxLength(50, ErrorMessage = "Title must be at most 50 characters")]

    public string Title { get; set; }
    public string Tag { get; set; }
    [Required (ErrorMessage = "Year is required")]
    public int Year { get; set; } 
    [Required (ErrorMessage = "Author is required")]
    public string Author { get; set; }
    
    
}
