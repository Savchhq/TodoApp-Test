import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { TaskComponent } from './components/todo-list/task.component';
import { CategoryComponent } from './components/categoty/category.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'tasks', component: TaskComponent },
  { path: 'categories', component: CategoryComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' } 
];