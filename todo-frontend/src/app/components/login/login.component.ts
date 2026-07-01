import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {

    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      
      this.authService.login(this.loginForm.value).subscribe({
        
        next: (response) => {
          console.log('Успішний вхід!');
          this.router.navigate(['/tasks']);
        },
        
        error: (err) => {
          console.error('Помилка входу:', err);
          alert('Невірний логін або пароль');
        }
      });
    }
  }
}
