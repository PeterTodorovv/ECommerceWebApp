import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AddProductService } from '../services/add-product.service';
import { addProductDto } from './productDtos/addProductDto';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
})
export class AddProductComponent {

  public addProductFrom!: FormGroup;

  constructor(private addProductService: AddProductService,){}
  ngOnInit(): void {
    this.addProductFrom = new FormGroup({
      name: new FormControl(''),
      describtion: new FormControl(''),
      imageUrl: new FormControl('')
    });
  }
  public addProduct(addProducFormtValue: any){
    const formValues = {...addProducFormtValue};
    const product: addProductDto = {
      name: formValues.name,
      description: formValues.describtion,
      imageUrl: formValues.imageUrl
    };

  }
}


