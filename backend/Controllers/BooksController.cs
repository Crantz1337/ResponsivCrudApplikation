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
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET api/books
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_bookService.Books);
    }

    // POST api/books
    [HttpPost]
    public ActionResult<Book> AddBook([FromBody] Book newBook)
    {
        _bookService.AddBook(newBook);
        return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
    }

    // PUT api/books/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateBook(Guid id, [FromBody] Book updatedBook)
    {
        var book = Books.Find(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.ISBN = updatedBook.ISBN;
        return NoContent();
    }

    // DELETE api/books/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteBook(Guid id)
    {
        var book = _bookService.Books.Find(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }
        _bookService.dflhkoBooks.Remove(book);
        return NoContent();
    }


}
