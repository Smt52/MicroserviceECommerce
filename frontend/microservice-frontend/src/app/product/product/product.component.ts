import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from '../product.model';
import { CardComponent } from '../../shared/card/card.component';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CardComponent],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
    @Input({required:true}) product! : Product;
    @Input({required:true}) selected! : boolean;
    @Output() selectedProductId = new EventEmitter<string>();

    get imagePath(){
        return `${this.product.imagePath}`;
    }


    onSelectProduct() {
        this.selectedProductId.emit(this.product.id);    
    }
}
