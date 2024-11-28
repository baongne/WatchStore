import { Routes } from '@angular/router';
import { HeaderComponent } from './shared/header/header.component';
import { HomeComponent } from './modules/home/home.component';
import { ProductsComponent } from './modules/products/products.component';
import { UserProfileComponent } from './modules/user-profile/user-profile.component';
import { HistoryComponent } from './modules/history/history.component';
import { CartComponent } from './modules/cart/cart.component';
import { ProductDetailComponent } from './modules/product-detail/product-detail.component';
import { DangnhapComponent } from './modules/auth/dangnhap/dangnhap.component';
import { DangkiComponent } from './modules/auth/dangki/dangki.component';
import { SearchPageComponent } from './modules/search-page/search-page.component';
import { PaymentComponent } from './modules/payment/payment.component';
import { InvoiceComponent } from './modules/invoice/invoice.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home', 
        component:HomeComponent
    },
    {
        path: 'products', 
        component:ProductsComponent
    },
    {
        path: 'user-profile', 
        component:UserProfileComponent
    },
    {
        path: 'history', 
        component:HistoryComponent
    },
    {
        path: 'cart', 
        component:CartComponent
    },
    {
        path: 'product-detail/:id', 
        component:ProductDetailComponent
    },
    {
        path:'dangnhap',
        component:DangnhapComponent
    },
    {
        path:'dangki',
        component:DangkiComponent
    },
    {
        path:'search-page/:key',
        component:SearchPageComponent
    },
    {
        path:'payment',
        component:PaymentComponent
    },
    {
        path:'invoice',
        component:InvoiceComponent
    }
    


];
