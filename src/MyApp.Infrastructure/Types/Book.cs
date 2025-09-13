namespace MyApp.Infrastructure.Types;

public class Book : BaseEntity
{
    public string Title { get; set; } = null!;
    public Author Author { get; set; } = null!;
    public int AuthorId { get; set; } 
}