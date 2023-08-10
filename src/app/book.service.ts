import { Injectable } from "@angular/core";
import { Book } from "./models";

@Injectable({ providedIn: 'root' })
export class BookService {

     private books: Book[] = [];
     id = 0;
 
  getBooks() {
    return this.books;
  }

  addBook(book: Book) {
    book.id = this.id;
    this.books.push(book);
    this.id++;
  }

  deleteBook(book: Book)
  {
    this.books = this.books.filter(item => item.id != book.id );

  }

  editBook(updatedBook: Book)
  {
    let index = this.books.findIndex(item => item.id == updatedBook.id);
    this.books[index].name = updatedBook.name;
    this.books[index].author = updatedBook.author;
    this.books[index].dateOfRelease = updatedBook.dateOfRelease;
  }

  getBookById(id: number): Book {
    return this.books.find(book => book.id === id)!;
  }

}