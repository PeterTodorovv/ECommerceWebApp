import { Injectable } from '@angular/core';
import { environment } from 'src/app/enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentUrlService {
  public urlAddress: string = 'https://localhost:7135';
  constructor() { }
}
