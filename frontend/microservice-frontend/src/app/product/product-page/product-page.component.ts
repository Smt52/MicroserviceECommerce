import { Component, signal} from '@angular/core';
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
  selectedProduct: Product = {
    id: '',
    name: '',
    description: '',
    price: 0,
    category: [],
    imagePath: ''
  };
  isLoading = signal(false);
  isProductFetching = signal(false);
  constructor(private productService:ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading.set(true);
    this.productService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
        if(this.products.length > 0){
          this.selectedProductId = this.products[0].id;
          this.onSelectProduct(this.selectedProductId);
        }
      },
      complete: () => {
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }


  onSelectProduct(productId: string): void {
    this.isProductFetching.set(true);
    this.selectedProductId = productId;
    this.productService.getProductById(productId).subscribe({
      next: (product: Product) => {
        this.selectedProduct = product;
      },
      complete: () => {
        this.isProductFetching.set(false);
      },
      error: (error) => {
        console.error('Error fetching product:', error);
      }
    });
  }

  createProduct(){
    
  }

  

}
