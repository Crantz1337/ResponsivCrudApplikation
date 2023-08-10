import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';
import { Book } from '../models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  books: Book[] = [];

  constructor(private bookService: BookService) { 
    this.getBooks();
  }


  getBooks(): void {
    this.books = this.bookService.getBooks();
  }

  deleteBook(book: Book): void {
    this.bookService.deleteBook(book);
    this.getBooks(); 
  }
}