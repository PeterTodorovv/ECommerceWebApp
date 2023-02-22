import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './add-product/add-product.component';
import { AdminGuard } from './authentication/guards/admin.guard';
import { AuthGuard } from './authentication/guards/auth.guard';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterUserComponent } from './authentication/register-user/register-user.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: 'Register',
    component:RegisterUserComponent
  },
  {
    path: 'Login',
    component: LoginComponent
  },
  {
    path: 'AddProduct',
    component: AddProductComponent, canActivate: [AdminGuard]
  },
  {
    path: '',
    component: HomeComponent
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
