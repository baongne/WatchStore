import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { Cart, CartShow } from '../../models/cart.model';
import { ProductService } from '../../services/product.service';
import { Product, productPayment } from '../../models/product.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, SidebarComponent, CommonModule, RouterModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cart: Cart[] = [];
  cartShow: CartShow[] = [];
  total_price = 0;
  constructor(private cartService: CartService, private productService: ProductService, private router: Router) { }
  getAllCarts(): Promise<Cart[]> {
    return new Promise((resolve, reject) => {
      this.cartService.getAllCarts().subscribe({
        next: (res) => {
          resolve(res);
        },
        error: (error) => {
          console.error('Error:', error);
          reject(error);
        }
      });
    });
  }

  getCookie(name: string): string | null {
    const nameEQ = `${name}=`;
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
  }
  idUser !: string;
  ngOnInit(): void {
    this.idUser = this.getCookie('id') || '';
    this.getCartsUserId(this.idUser);
  }

  async getCartsUserId(idUser: string) {
    let cartshow:CartShow[]=[];
    this.cart = [];
    let allCarts: Cart[] = await this.getAllCarts();
    for (let i = 0; i < allCarts.length; i++) {
      if (allCarts[i].userId === Number(idUser)) {
        this.cart.push(allCarts[i]);
      }
    }
    let promises = this.cart.map(cart => this.getProductByid(cart.productId.toString()));
    let results = await Promise.all(promises);
    for (let i = 0; i < this.cart.length; i++) {
      cartshow[i] = {
        quantity: -1,
        userId: -1,
        productId: -1,
        product: {} as Product
      };
      cartshow[i].id = this.cart[i].id;
      cartshow[i].productId = this.cart[i].productId;
      cartshow[i].quantity = this.cart[i].quantity;
      cartshow[i].userId = this.cart[i].userId;
      cartshow[i].product = results[i];
    }
    this.cartShow = cartshow;
    this.caculateTotalPrice();
    console.log(this.cartShow);
  }
  caculateTotalPrice() {
    this.total_price = 0;
    if (this.cartShow.length > 0) {
      for (let i = 0; i < this.cartShow.length; i++) {
        this.total_price += this.cartShow[i].quantity * (this.cartShow[i].product.priceToSell * (1 - this.cartShow[i].product.discount));
      }
    } else {
      this.total_price = 0;
    }
  }
  getProductByid(id: string): Promise<Product> {
    return new Promise((resolve, reject) => {
      this.productService.getProductById(id).subscribe({
        next: (res) => {
          resolve(res);
        },
        error: (error) => {
          reject(error);
        }
      });
    });
  }
  // FORMAT VND
  formatCurrencyVND(amount: number): string {
    return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }
  deleteCart(cartId: number): Promise<void> {
    return new Promise((resolve, reject) => {
      this.cartService.deleteCart(cartId).subscribe({
        next: () => {
          resolve();
        },
        error: (error) => {
          console.error('Error:', error);
          reject(error);
        }
      });
    });
  }
  updateCart(cartData: Cart, cartId: number): Promise<void> {
    return new Promise((resolve, reject) => {
      this.cartService.updateCart(cartData, cartId).subscribe({
        next: () => {
          resolve();
        },
        error: (error) => {
          console.error('Error:', error);
          reject(error);
        }
      });
    });
  }
  async deleteProductFromCart(id: number | undefined) {
    if (id === undefined) {
      alert('Không thể xóa sản phẩm này khỏi giỏ hàng');
      return;
    }
    await this.deleteCart(id);
    await this.getCartsUserId(this.idUser);

  }
  async minusOne(id: number | undefined) {
    if (id !== undefined) {
      let cartData: Cart = {
        quantity: -1,
        userId: Number(this.idUser),
        productId: -1,
      }
      for (let i = 0; i < this.cartShow.length; i++) {
        if (id === this.cartShow[i].id) {
          if (this.cartShow[i].quantity > 1) {
            cartData.quantity = this.cartShow[i].quantity - 1;
            cartData.productId = this.cartShow[i].productId;
            await this.updateCart(cartData, id);
            await this.getCartsUserId(this.idUser);
            return;
          }
        }
      }
    }
  }
  async plusOne(id: number | undefined) {
    if (id !== undefined) {
      let cartData: Cart = {
        quantity: -1,
        userId: Number(this.idUser),
        productId: -1,
      }
      for (let i = 0; i < this.cartShow.length; i++) {
        if (id === this.cartShow[i].id) {
          if (this.cartShow[i].quantity < this.cartShow[i].product.stockQuantity) {
            cartData.quantity = this.cartShow[i].quantity + 1;
            cartData.productId = this.cartShow[i].productId;
            await this.updateCart(cartData, id);
            await this.getCartsUserId(this.idUser);
            return;
          }
        }
      }
    }
  }
  goToPayment() {
    let products: productPayment[] = [];
    for (let i = 0; i < this.cartShow.length; i++) {
      products[i] = {
        product: this.cartShow[i].product,
        quantity: this.cartShow[i].quantity
      };
    }
    const encodedCartItems = encodeURIComponent(JSON.stringify(products));
    this.router.navigate(['/payment'], { queryParams: { cart: encodedCartItems } });
  }
}
