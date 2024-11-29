import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { ProductPageComponent } from './product/product-page/product-page.component';

export const routes: Routes = [
    {path: '', component: HomePageComponent},
    {path:'products',component:ProductPageComponent},
    {path: '', redirectTo: '', pathMatch: 'full'}
];
