namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    public List<Book> Books { get; set; }
    public List<Quote> Quotes { get; set; }

}

