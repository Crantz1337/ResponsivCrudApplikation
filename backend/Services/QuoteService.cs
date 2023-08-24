using System;
using WebApi.Entities;
using WebApi.Services;

public interface IQuoteService
{
    void AddQuote(Quote book, Guid userId);
    void DeleteQuote(Guid quoteId, Guid userId);
    IEnumerable<Quote> GetQuotes(Guid userId);
}
public class QuoteService : IQuoteService
{
    private IUserService _userService;

    public QuoteService(IUserService userService)
    {
        _userService = userService;
    }
    public IEnumerable<Quote> GetQuotes(Guid userId)
    {
        var user = _userService.GetById(userId);
        return user.Quotes;
    }

    public void AddQuote(Quote quote, Guid userId)
    {
        var user = _userService.GetById(userId);
        if (user.Quotes.FindIndex(item => item.Id == quote.Id) != -1)
        {
            throw new Exception("Quote with the given id already exists");
        }
        user.Quotes.Add(quote);
    }

    public void DeleteQuote(Guid bookId, Guid userId)
    {
        var user = _userService.GetById(userId);
        var amnt = user.Books.RemoveAll(x => x.Id == bookId);
        if (amnt == 0)
        {
            throw new Exception("No elements to be deleted exists that matches the id");
        }

    }


}
