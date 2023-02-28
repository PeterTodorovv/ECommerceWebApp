import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AddProductService } from '../services/add-product.service';
import { BaseResponseDto } from '../user/baseResponseDto';
import { addProductDto } from './productDtos/addProductDto';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
})
export class AddProductComponent implements OnInit {

  public addProductFrom!: FormGroup;
  private returnUrl!: string;
  errorMessage!: string;
  showError!: boolean;
  data = new FormData();


  constructor(private addProductService: AddProductService, private router: Router, private route: ActivatedRoute){}
  ngOnInit(): void {
    this.addProductFrom = new FormGroup({
      name: new FormControl(''),
      price: new FormControl(''),
      description: new FormControl(''),
      image: new FormControl('')
    });
    this.returnUrl = this.route.snapshot.queryParams[''] || '/';
  }
  addProduct = async (addProducFormtValue: any) =>
  {
    this.showError = false;
    const formValues = {...addProducFormtValue};
    const productDto: addProductDto = {
      Name: formValues.name,
      Price: formValues.price,
      Description: formValues.description,
      Image: formValues.image
    };

    this.data.append("Name", formValues.name);
    this.data.append("Price", formValues.price);
    this.data.append("Description", formValues.description);

    var response = fetch(formValues.image);
    const blob = await (await response).blob();

    this.data.append("Image", blob, formValues.image.name);


    this.addProductService.addProduct("Product", this.data)
    .subscribe({
      next: (res:BaseResponseDto) => {
        this.router.navigate([this.returnUrl]);
    },
    error: (err:HttpErrorResponse) => {
      var response  = err.error as BaseResponseDto;
      this.errorMessage = response.errors[response.errors.length - 1].message;
      this.showError = true;
    }
    })
  }
}


