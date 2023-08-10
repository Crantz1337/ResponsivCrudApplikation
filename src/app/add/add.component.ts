import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Book } from '../models';
import { BookService } from '../book.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})

// https://angular.io/guide/inputs-outputs

export class AddComponent {
  
  bookId = 0;
  bookForm: FormGroup;
  errorMessage: string;

  constructor(private bookService: BookService, private router: Router) {
    this.bookForm = new FormGroup({
      'name': new FormControl(null, Validators.required),
      'author': new FormControl(null, Validators.required),
      'dateOfRelease': new FormControl(null, Validators.required)
    });

    
    this.errorMessage = '';
    
  }

  onSubmit() {
    if(this.bookForm.valid) {
      this.bookService.addBook({...this.bookForm.value, id: this.bookId});
      this.bookId++;
      this.errorMessage = '';
      this.bookForm.reset(); 
      this.router.navigate(['/home']);
    } else {
      this.errorMessage = 'Please fill out all fields!';
    }
  }
}

