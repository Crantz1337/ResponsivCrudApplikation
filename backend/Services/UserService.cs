namespace WebApi.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    void AddBook(Book book, string userId);

    void AddQuote(Quote quote, string userId);
    void DeleteQuote(string quoteId, string userId);
    void DeleteBook(string bookId, string userId);
    void UpdateBook(Book book, string userId);

    IEnumerable<Book> GetBooksByUserId(string userId);
    IEnumerable<Quote> GetQuotesByUserId(string userId);

    User GetById(string id);
}

public class UserService : IUserService
{

    static private List<User> _users = new List<User>
    {
        new User { Id = "5hvc5pi94t", Username = "admin", Password = "password", Books = new List<Book>(), Quotes = 
            new List<Quote>{
               new Quote {Id = "4pb0x88c94",Name = "\"Anything worth dying for is certainly worth living for.\"", Author = "Joseph Heller" },
			   new Quote {Id = "qpr6joyctu", Name = "\"Not all those who wander are lost.\"", Author = "J.R.R. Tolkein" },
			   new Quote {Id = "pce0gq7mle", Name = "\"There is always something left to love.\"", Author = "Gabriel García Márquez" },
			   new Quote {Id = "l84mfc3204", Name = "\"Everything was beautiful, and nothing hurt.\"", Author = "Kurt Vonnegut" },
			   new Quote {Id = "0yki2e0nan", Name = "\"It's the possibility of having a dream come true that makes life interesting.\"", Author = "Paulo Coelho" },

			} 
        
        }
    };

    private readonly AppSettings _appSettings;

    public IEnumerable<Book> GetBooksByUserId(string userId)
    {
        var user = GetById(userId);
        return user?.Books;
    }

    public IEnumerable<Quote> GetQuotesByUserId(string userId)
    {
        var user = GetById(userId);
		return user?.Quotes;
	}

	public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        if (user == null) return null;

        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public void AddBook(Book book, string userId)
    {
        var user = GetById(userId);
		if (user.Books.FindIndex(item => item.Id == book.Id) != -1)
		{
			throw new Exception("Book with the given id already exists");
		}
		user.Books.Add(book);     
    }


    public void DeleteBook(string bookId, string userId)
    {
		var user = GetById(userId);
        var amnt = user.Books.RemoveAll(x => x.Id == bookId);
        if(amnt == 0)
        {
			throw new Exception("No elements to be deleted exists that matches the id");
		}

	}

    public void UpdateBook(Book book, string userId)
    {
		var user = GetById(userId);

		var index = user.Books.FindIndex(item => item.Id == book.Id);
		if (index == -1)
		{
			throw new Exception("Book cannot be updated because it doesn't exist");
		}

		user.Books[index].Name = book.Name;
	    user.Books[index].Author = book.Author;
	    user.Books[index].DateOfRelease = book.DateOfRelease;
		
	}

    public void AddQuote(Quote quote, string userId) 
    {
		var user = GetById(userId);
		if (user.Quotes.FindIndex(item => item.Id == quote.Id) != -1)
		{
			throw new Exception("Quote with the given id already exists");
		}
		user.Quotes.Add(quote);
	}

    public void DeleteQuote(string quoteId, string userId)
	{
		var user = GetById(userId);
		var amnt = user.Quotes.RemoveAll(x => x.Id == quoteId);
		if (amnt == 0)
		{
			throw new Exception("No elements to be deleted exists that matches the id");
		}

	}


	public User GetById(string userId)
    {
        var user = _users.Find(x => x.Id == userId);
        if (user == null)
        {
            throw new Exception("Invalid User");
        }
        return user;
    }


    private string generateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}