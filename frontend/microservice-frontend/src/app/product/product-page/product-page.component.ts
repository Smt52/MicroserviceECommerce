import { Component} from '@angular/core';
import { NgFor,NgIf } from '@angular/common';
import { Product } from '../product.model';
import { ProductService } from '../product.service';
import { error, log } from 'console';
import { ProductComponent } from "../product/product.component";
import { ProductViewComponent } from "../product-view/product-view.component";

@Component({
  selector: 'app-product-page',
  standalone: true,
  imports: [ProductComponent, ProductViewComponent,NgFor,NgIf],
  templateUrl: './product-page.component.html',
  styleUrl: './product-page.component.css'
})
export class ProductPageComponent {
  products: Product[] = [];
  selectedProductId? : string;
  constructor(private productService:ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (products: Product[]) => {
        this.products = products;
        if(this.products.length > 0){
          this.selectedProductId = this.products[0].id;
        }
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  get selectedProduct():Product{
    return this.products.find(product => product.id === this.selectedProductId) ?? { id: '', name: 'ProductNotFound',category:[], description: '', price: 0,imagePath: ''};
  }

  onSelectProduct(productId:string){
    this.selectedProductId = productId;
    this.productService.getProductById(productId).subscribe({
      next: (product: Product) => {
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  

}
