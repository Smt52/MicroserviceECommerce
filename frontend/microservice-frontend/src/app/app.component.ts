import { Component, NgModule, importProvidersFrom } from '@angular/core';
import { ProductPageComponent } from "./product/product-page/product-page.component";
import { provideHttpClient, withFetch } from '@angular/common/http';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ ProductPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'microservice-frontend';
}
