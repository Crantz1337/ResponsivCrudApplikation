import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup;
  errorMessage: string = '';

  constructor(
    private formBuilder: FormBuilder, 
    private apiService: ApiService, 
    private router: Router
  ) {
      this.form = new FormGroup({
        'username': new FormControl(null, Validators.required),
        'password': new FormControl(null, Validators.required),
      });
    
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.apiService.authenticate(this.form.value).subscribe(response => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['/home']);
      }, error => {
        this.errorMessage = 'Invalid credentials, please try again.';
      });
    }
  }
}
