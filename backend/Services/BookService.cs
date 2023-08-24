namespace WebApi.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

public interface IBookService
{
    void AddBook(Book book, Guid userId);
    void DeleteBook(Guid bookId, Guid userId);
    void UpdateBook(Book book, Guid userId);

    IEnumerable<Book> GetBooks(Guid userId);
}

public class BookService : IBookService
{
    private IUserService _userService;

    public BookService(IUserService userService)
    {
        _userService = userService;
    }
    public IEnumerable<Book> GetBooks(Guid userId)
    {
        var user = _userService.GetById(userId);
        return user?.Books;
    }

    public void AddBook(Book book, Guid userId)
    {
        var user = _userService.GetById(userId);
		if (user.Books.FindIndex(item => item.Id == book.Id) != -1)
		{
			throw new Exception("Book with the given id already exists");
		}
		user.Books.Add(book);     
    }

    public void DeleteBook(Guid bookId, Guid userId)
    {
		var user = _userService.GetById(userId);
        var amnt = user.Books.RemoveAll(x => x.Id == bookId);
        if(amnt == 0)
        {
			throw new Exception("No elements to be deleted exists that matches the id");
		}

	}



}