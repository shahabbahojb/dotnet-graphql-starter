[QueryType]
public static class Query
{
    public static string SayHello(string name = "World")
        => $"Hello, {name}!";
}