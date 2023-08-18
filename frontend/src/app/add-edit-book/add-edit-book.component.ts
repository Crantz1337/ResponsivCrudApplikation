import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-add-edit-book',
  templateUrl: './add-edit-book.component.html',
  styleUrls: ['./add-edit-book.component.css']
})
export class AddEditBookComponent  {
  isEdit = false;
  form: FormGroup;
  editId: string;
  errorMessage: string;

  constructor(
    public apiService: ApiService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.form = new FormGroup({
      'name': new FormControl(null, Validators.required),
      'author': new FormControl(null, Validators.required),
      'dateOfRelease': new FormControl(null, Validators.required)
    });

    const idFromRoute = this.route.snapshot.paramMap.get('id');
if (idFromRoute) {
    this.editId = idFromRoute;
    this.isEdit = true;
    let bookToEdit = this.apiService.books.find(item => item.id == this.editId);
    if(bookToEdit) {
        this.form.setValue({
            'name': bookToEdit.name,
            'author': bookToEdit.author,
            'dateOfRelease': bookToEdit.dateOfRelease
        });
    }
} else {
    this.editId = '';
    this.isEdit = false;
}
    
    this.errorMessage = '';
    
  }

  onSubmit(): void {

    if(this.form.valid)
    {

      this.errorMessage = '';

      if (this.isEdit) {
        this.apiService.editBook({...this.form.value, id: this.editId}).subscribe(response => {
          this.router.navigate(['/home']);
        }, error => {
          console.log(error);
        });
  
      } else {
        this.apiService.addBook({...this.form.value, id: this.apiService.generateRandomId()}).subscribe(response => {
          this.router.navigate(['/home']);
        }, error => {
          console.log(error);
        });
  
      }
    } else  {
      this.errorMessage = "Fields can't be empty";
    }


  }
}
