namespace MyApp.Api;

[QueryType]
public static class Query
{
    public static string SayHello(string name = "World")
        => $"Hello, {name}!";

    /*public static Book GetBookByName(string title = "How to Code C#")
        => new Book(title, new Author("Shahab Bahojb"));*/
}