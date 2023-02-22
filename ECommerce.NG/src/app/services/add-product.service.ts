import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { addProductDto } from '../add-product/productDtos/addProductDto';
import { EnvironmentUrlService } from './environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class AddProductService {

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, private rout: Router) { }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  public addProduct = (route: string, body: addProductDto) => {
    return this.http.post<addProductDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }
}
