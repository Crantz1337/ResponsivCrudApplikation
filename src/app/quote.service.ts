import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {

  private quotes: string[] = [];

  constructor() { }

  addQuote(quote: string)
  {
    this.quotes.push(quote);
  }

  deleteQuote(quote: string)
  {
    this.quotes = this.quotes.filter(item => item != quote);
  }

  getQuotes()
  {
    return this.quotes;
  }

}
