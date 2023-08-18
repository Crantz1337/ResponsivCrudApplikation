import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-quotes',
  templateUrl: './quotes.component.html',
  styleUrls: ['./quotes.component.css']
})
export class QuotesComponent implements OnInit {

  form: FormGroup;
  errorMessage: string;

  constructor(public apiService: ApiService, private router: Router) {
    
    this.form = new FormGroup({
      'name': new FormControl(null, Validators.required),
      'author': new FormControl(null, Validators.required),
    });
    
    this.errorMessage = '';

  }


ngOnInit(): void {
  this.apiService.getQuotes();
}

toHome()
{
  this.router.navigate(['/home']);
}

deleteQuote(quoteId: string)
{
    this.apiService.deleteQuote(quoteId).subscribe(() => 
    {
      this.apiService.quotes = this.apiService.quotes.filter(quote => quote.id !== quoteId);
    }, error => {
      console.log(error);
    }  
    );

    
}

onSubmit()
{ 
  if(this.form.valid)
  {
    this.apiService.addQuote({...this.form.value, id: this.apiService.generateRandomId()}).subscribe(() => 
    {
      this.apiService.getQuotes();
    }, error => {
      console.log(error);
    }  
    );
  } else  {
    this.errorMessage = "Fields can't be empty"
  }
   
}
   

}