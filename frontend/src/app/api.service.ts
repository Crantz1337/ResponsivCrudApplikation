import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';
import { AuthenticateRequest } from './models/AuthenticateRequest';
import { AddRequest } from './models/AddRequest';
import { DeleteRequest } from './models/DeleteRequest';
import { Book } from './entities/book';
import { Quote } from './entities/quote';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  
  private baseURL = 'http://localhost:4000/api';
  public books: Book[] = [];
  public quotes: Quote[] = [];



  constructor(private http: HttpClient, private router: Router) { }

  private getHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    const token = localStorage.getItem('token');

    if (token) {
      headers = headers.append('Authorization', `Bearer ${token}`);
    }

    return headers;
  }


  generateRandomId(): string {
      return uuidv4();
  }


  authenticate(model: AuthenticateRequest): Observable<any> {
    return this.http.post(`${this.baseURL}/authenticate`, model);
  }

  logOut(): void {
    const token = localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  addBook(book: Book): Observable<any> {
    const headers = this.getHeaders();
    let model = new AddRequest("book", book);
    return this.http.post(`${this.baseURL}/add`, model, { headers });
  }

  deleteBook(bookId: string): Observable<any> {
    const headers = this.getHeaders();
    let model = new DeleteRequest("book", bookId);
    return this.http.post(`${this.baseURL}/delete`, model, { headers });
  }

  editBook(book: Book): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post(`${this.baseURL}/edit`, book, { headers });
  }

  addQuote(quote: Quote): Observable<any> {
    const headers = this.getHeaders();
    let model = new AddRequest("quote", quote);
    return this.http.post(`${this.baseURL}/add`, model, { headers });
  }

  deleteQuote(quoteId: string): Observable<any> {
    const headers = this.getHeaders();
    let model = new DeleteRequest("quote", quoteId);
    return this.http.post(`${this.baseURL}/delete`, model, { headers });
  }


  getBooks(): boolean {
    const headers = this.getHeaders();
    this.http.get<Book[]>(`${this.baseURL}/books`, { headers }).subscribe(response => {
      this.books = response;
    }, error => {
      return false;
    });
    return true;
  }

  getQuotes(): boolean {
    const headers = this.getHeaders();
    this.http.get<Quote[]>(`${this.baseURL}/quotes`, { headers }).subscribe(response => {
      this.quotes = response;
    }, error => {
      return false;
    });
    return true;
  }
}
