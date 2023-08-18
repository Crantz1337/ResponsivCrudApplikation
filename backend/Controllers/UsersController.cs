namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

[ApiController]
[Route("api")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] AddRequest model)
    {
		var user = (User)HttpContext.Items["User"];
		if (user == null)
		{
			return Unauthorized(new { message = "Authentication failed!" });
		}

		try
		{
			if (model.Type == "book")
			{
				_userService.AddBook(JsonConvert.DeserializeObject<Book>(model.Data.ToString()), user.Id);
				return Ok(new { message = "Book deleted successfully!" });
			}
			else if (model.Type == "quote")
			{
				_userService.AddQuote(JsonConvert.DeserializeObject<Quote>(model.Data.ToString()), user.Id);
				return Ok(new { message = "Quote deleted successfully!" });
			}
			else
			{
				return BadRequest(new { message = "Invalid Type" });
			}
		}
		catch (Exception ex)
		{
			return BadRequest($"{ex.Message}");
		}

	

	}

    [HttpPost("delete")]
    public IActionResult Delete([FromBody] DeleteRequest model)
    {
		var user = (User)HttpContext.Items["User"];
		if (user == null)
		{
			return Unauthorized(new { message = "Authentication failed!" });
		}

		try
		{

			if (model.Type == "book")
			{
				_userService.DeleteBook(model.Id, user.Id);
				return Ok(new { message = "Book deleted successfully!" });
			}
			else if (model.Type == "quote")
			{
				_userService.DeleteQuote(model.Id, user.Id);
				return Ok(new { message = "Quote deleted successfully!" });
			}
			else
			{
				return BadRequest(new { message = "Invalid Type" });
			}
		}
		catch (Exception ex) 
		{
			return BadRequest($"{ex.Message}");
		}
		


	}

    [HttpPost("edit")]
    public IActionResult Edit([FromBody] Book book) 
    {
		var user = (User)HttpContext.Items["User"];
		if (user == null)
		{
			return Unauthorized(new { message = "Authentication failed!" });
		}

		try
		{
			_userService.UpdateBook(book, user.Id);
			return Ok(new { message = "Book updated successfully!" });

		}
		catch (Exception ex)
		{
			return BadRequest($"{ex.Message}");
		}

	}

    [HttpGet("books")]
    public IActionResult GetBooks()
    {         
        var user = (User)HttpContext.Items["User"]; 
        if (user == null)
        {
            return Unauthorized(new { message = "Authentication failed!" });
        }

		try
		{
			var books = _userService.GetBooksByUserId(user.Id);
			if (books == null || !books.Any())
			{
				return NotFound(new { message = "No books found for this user." });
			}

			return Ok(books);
		}
		catch (Exception ex)
		{
			return BadRequest($"{ex.Message}");
		}

	
	}

    [HttpGet("quotes")]
    public IActionResult GetQuotes()
    {
		var user = (User)HttpContext.Items["User"];
		if (user == null)
		{
			return Unauthorized(new { message = "Authentication failed!" });
		}

        try
        {
			var quotes = _userService.GetQuotesByUserId(user.Id);
			if (quotes == null || !quotes.Any())
			{
				return NotFound(new { message = "No quotes found for this user" });
			}

			return Ok(quotes);
		}
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }

     
	}


}
