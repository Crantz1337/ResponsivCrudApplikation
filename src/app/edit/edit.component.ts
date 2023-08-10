import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BookService } from '../book.service';
import { Book } from '../models';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})

export class EditComponent {

  editForm: FormGroup;
  bookId: number;
  errorMessage: string;

  constructor(private bookService: BookService, private activatedroute:ActivatedRoute, private router: Router) {
    this.editForm = new FormGroup({
      'name': new FormControl(null, Validators.required),
      'author': new FormControl(null, Validators.required),
      'dateOfRelease': new FormControl(null, Validators.required)
    });

    this.bookId = Number(activatedroute.snapshot.paramMap.get("id"));
    const book: Book = this.bookService.getBookById(this.bookId);
    if (book) {
      this.editForm.setValue({
        'name': book.name,
        'author': book.author,
        'dateOfRelease': book.dateOfRelease
      });
    } 
    this.errorMessage = '';
  }

  onSubmit() {
    if(this.editForm.valid) {
      this.bookService.editBook({...this.editForm.value, id: this.bookId});
      this.bookId++;
      this.errorMessage = ''; 
      this.editForm.reset(); 
      this.router.navigate(['/home']);
    } else {
      this.errorMessage = 'Please fill out all fields!';
    }
  }
}