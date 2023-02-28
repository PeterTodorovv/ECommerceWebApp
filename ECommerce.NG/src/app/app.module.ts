import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddProductComponent } from './add-product/add-product.component';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}

imports: [
  BrowserModule,
  HttpClientModule,
  RouterModule.forRoot([
    { path: 'home', component: HomeComponent },
    { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: '**', redirectTo: '/404', pathMatch: 'full'},

])]

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    AddProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7135"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
