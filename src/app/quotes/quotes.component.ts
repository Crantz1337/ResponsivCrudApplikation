import { Component } from '@angular/core';
import { QuoteService } from '../quote.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-quotes',
  templateUrl: './quotes.component.html',
  styleUrls: ['./quotes.component.css']
})
export class QuotesComponent {
 
  quotes: string[] = [];
  quotesForm: FormGroup;
  errorMessage: string;

  constructor(private quoteService: QuoteService) { 
    this.quotesForm = new FormGroup({'quote': new FormControl(null, Validators.required)});
    this.errorMessage = '';

    this.quoteService.addQuote("Quote 1");
    this.quoteService.addQuote("Quote 2");
    this.quoteService.addQuote("Quote 3");
    this.quoteService.addQuote("Quote 4");
    this.quoteService.addQuote("Quote 5");

    this.getQuotes();
  }

 
  getQuotes(): void {
    this.quotes = this.quoteService.getQuotes();
  }

  deleteQuote(quote: string): void {
    this.quoteService.deleteQuote(quote);
    this.getQuotes(); 
  }

  onSubmit()
  {
    if(this.quotesForm.valid)
    {
      this.quoteService.addQuote(this.quotesForm.value.quote); 
      this.errorMessage = '';
      this.quotesForm.reset();
    } else {
      this.errorMessage = "Please fill out the field";
    }


  }

}
