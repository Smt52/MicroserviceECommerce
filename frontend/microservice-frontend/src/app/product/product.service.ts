import { Injectable } from "@angular/core";
import { Product } from "./product.model";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";


@Injectable({providedIn:'root'})
export class ProductService {

    private apiUrl = environment.baseUrl + '/product-service';

    constructor(private httpClient : HttpClient) {}
        

    getProducts(): Observable<Product[]> {
        return this.httpClient.get<{ products: Product[] }>(`${this.apiUrl}/products`,{observe:'response'})
          .pipe(map(response => response.body!.products));
      }


      getProductById(id: string): Observable<Product> {
        return this.httpClient.get<{product :Product}>(`${this.apiUrl}/products/${id}`)
        .pipe(
            map(response=>response.product)
        );
      }
      
      
    
    getProductsByCategory(category:string):Observable<Product[]>{
        return this.httpClient.get<Product[]>(this.apiUrl + "products/category/" + category);
    }


    addProduct(product:Product):Observable<Product>{
        return this.httpClient.post<Product>(this.apiUrl + '/products',product);
    }
}

