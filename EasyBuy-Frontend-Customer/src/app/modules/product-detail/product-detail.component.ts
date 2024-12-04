import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Product, productBuy, productPayment } from '../../models/product.model';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Cart } from '../../models/cart.model';
import { CartService } from '../../services/cart.service';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';


@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, SidebarComponent, CommonModule, FormsModule, RouterModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent implements OnInit {
  id !: string;
  product !: Product;
  quantity = 1;
  isOpenPD = false;
  isOpenSI = false;
  openPD() {
    this.isOpenPD = !this.isOpenPD;
  }
  openSI() {
    this.isOpenSI = !this.isOpenSI;
  }
  constructor(private productService: ProductService, private route: ActivatedRoute, private dataService: DataService, private router: Router, private cartService: CartService
    , private userService: UserService
  ) { }
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') as string;
    this.getProductById();
  }
  async getProductById() {
    try {
      this.product = await firstValueFrom(this.productService.getProductById(this.id));
      console.log(this.product);
    } catch (error) {
      console.error('Lỗi khi gọi API:', error);
    }
  }
  // FORMAT VND
  formatCurrencyVND(amount: number): string {
    return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }

  onBtnBuy() {
    if (this.product.stockQuantity < this.quantity || this.quantity < 1) {
      alert('Số lượng sản phẩm không đủ!!');
      return;
    }

    let product: productPayment = {
      product: this.product,
      quantity: this.quantity
    }
    let data: productPayment[] = [];
    data.push(product);
    const encodedCartItems = encodeURIComponent(JSON.stringify(data));
    this.router.navigate(['/payment'], { queryParams: { data: encodedCartItems } });

  }

  sendData(data: productBuy[]) {
    this.dataService.changeData(data);
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
  getAllCart(): Promise<Cart[]> {
    return new Promise((resolve, reject) => {
      this.cartService.getAllCarts().subscribe({
        next: (res) => resolve(res),
        error: (error) => reject(error)
      });
    });
  }

  async onBtnAddToCart() {
    if (this.product.stockQuantity < this.quantity || this.quantity < 1) {
      alert('Số lượng sản phẩm không đủ!!');
      return;
    }
    let userId = this.getCookie('id') || '';
    //let pro = await this.getProById(this.id);
    // let user = await this.getUserById(userId);
    let cartData: Cart = {
      quantity: this.quantity,
      userId: Number(userId),
      productId: Number(this.id),
      // user: user,
      // product:pro
    }
    let idCart;
    let isExist = false;
    let carts: Cart[] = await this.getAllCart();
    for (let i = 0; i < carts.length; i++) {
      if (carts[i].productId === Number(this.id) && carts[i].userId === Number(userId)) {
        idCart = carts[i].id;
        isExist = true;
        if (cartData.quantity + this.quantity > this.product.stockQuantity) {
          cartData.quantity = this.product.stockQuantity;
        } else {
          cartData.quantity += this.quantity;
        }
      }
    }
    if (!isExist) {
      this.cartService.addCart(cartData).subscribe({
        next: (res) => {
          alert("Thêm vô giỏ hàng thành công");
        },
        error: (error) => {
          console.error('Có lỗi:', error);
        }
      })
    } else {
      this.cartService.updateCart(cartData, Number(idCart)).subscribe({
        next: (res) => {
          alert("Thêm vô giỏ hàng thành công");
        },
        error: (error) => {
          console.error('Có lỗi:', error);
        }
      })
    }


  }
}
