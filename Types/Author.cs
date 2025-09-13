namespace Demo.Gql.Types;

public class Author : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
