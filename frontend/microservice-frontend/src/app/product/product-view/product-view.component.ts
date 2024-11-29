import { Component, Input } from '@angular/core';
import { Product } from '../product.model';
import { ProductService } from '../product.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-product-view',
  standalone: true,
  imports: [NgFor],
  templateUrl: './product-view.component.html',
  styleUrl: './product-view.component.css'
})
export class ProductViewComponent {
  @Input({required:true}) product!: Product;

}
