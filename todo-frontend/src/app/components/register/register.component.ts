import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule], 
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe({
        next: () => {
          console.log('Реєстрація успішна!');
          alert('Акаунт створено! Тепер ви можете увійти.');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error('Помилка реєстрації:', err);
          alert('Помилка реєстрації. Можливо, такий email вже існує.');
        }
      });
    }
  }
}