import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public apiService: ApiService, private router: Router) {}

  logOut(): void
  {
    this.apiService.logOut();
  }
    
  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.apiService.getBooks();
  }

  addBook(): void {
    this.router.navigate(['/add']);
  }

  quotes(): void {
    this.router.navigate(['/quotes']);
  }  

  editBook(bookId: string): void {
    this.router.navigate(['/edit', bookId]);
  }

  deleteBook(bookId: string): void {
    this.apiService.deleteBook(bookId).subscribe(() => {
      this.apiService.books = this.apiService.books.filter(book => book.id !== bookId);
    }, error => {
      console.log(error);
    });

 
  }
}
