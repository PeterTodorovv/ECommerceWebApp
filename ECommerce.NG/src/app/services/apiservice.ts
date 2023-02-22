import { Injectable } from "@angular/core";
import { environment } from "../enviroments/environment";

@Injectable({
  providedIn: 'root',
})

export class Apiservice{
  private baseUrl: string = environment.urlAddress
}
