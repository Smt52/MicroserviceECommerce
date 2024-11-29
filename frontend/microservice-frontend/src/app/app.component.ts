import { Component, NgModule, importProvidersFrom } from '@angular/core';
import { ProductPageComponent } from "./product/product-page/product-page.component";
import { provideHttpClient, withFetch } from '@angular/common/http';
import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from "./shared/footer/footer.component";
import { NavbarComponent } from "./shared/navbar/navbar.component";
import { HomePageComponent } from './home-page/home-page.component';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, NavbarComponent,RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'microservice-frontend';
}
