import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { DataService } from '../../services/data.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product, productPayment } from '../../models/product.model';
import { firstValueFrom } from 'rxjs';
import { VoucherService } from '../../services/voucher.service';
import { Voucher } from '../../models/voucher.model';
import { FormsModule } from '@angular/forms';
import { Payment } from '../../models/payment.model';
import { PaymentService } from '../../services/payment.service';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { Order } from '../../models/order.model';
import { OrderService } from '../../services/order.service';
import { OrderlineService } from '../../services/orderline.service';
import { Orderline } from '../../models/orderline.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, HeaderComponent, SidebarComponent, FooterComponent],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  shipMethod = "fast";
  shippingPrice: number = 20000;
  fromCart: boolean = false;
  products: productPayment[] = [];
  total_price: number = 0;
  voucherPrice: number = 0;
  vouchers: Voucher[] = [];
  inputVoucher: string = '';
  voucherId: number = -1;
  payments: Payment[] = [];
  paymentId: number = -1;
  userId: number = -1;
  user: User = {
    name: 'null',
    address: 'null',
    email: 'null',
    password: 'null',
    phone: 'null',
    role: 'null',
    status: '1',
    id: '-1'
  };
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

  changeShipMethod(a: string) {
    this.shipMethod = a;
    this.shippingPrice = a === 'fast' ? 20000 : 60000;
    this.calculateTotalPrice();
  }
  changePayMethod(a: number) {
    this.paymentId = a;
  }
  formatCurrencyVND(amount: number): string {
    return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }

  constructor(private dataService: DataService, private route: ActivatedRoute, private productService: ProductService, private voucherService: VoucherService, private paymentService: PaymentService, private userService: UserService
    , private orderService: OrderService, private orderlineService: OrderlineService, private cartService: CartService, private router: Router
  ) { }
  async ngOnInit() {
    this.userId = Number(this.getCookie('id'));
    console.log(this.userId);
    this.route.queryParams.subscribe(params => {
      if (params['data']) {
        let data = JSON.parse(decodeURIComponent(params['data']));
        this.products = data;
        console.log(this.products);
      } else if (params['cart']) {
        let data = JSON.parse(decodeURIComponent(params['cart']));
        this.products = data;
        this.fromCart = true;
      }
    });
    this.calculateTotalPrice();
    this.getAllVouchers();
    this.getAllPayments();
    this.getUserById(this.userId.toString());
  }

  async getProductById(id: string): Promise<Product | null> {
    try {
      return await firstValueFrom(this.productService.getProductById(id));
    } catch (error) {
      console.error('Lỗi:', error);
      return null;
    }
  }
  async getProductsByIds(ids: string[]): Promise<(Product | null)[]> {
    const promises = ids.map(id => this.getProductById(id));

    return await Promise.all(promises);
  }
  calculateTotalPrice() {
    this.total_price = 0;
    for (let i = 0; i < this.products.length; i++) {
      this.total_price += this.products[i].quantity * ((1 - this.products[i].product.discount) * this.products[i].product.priceToSell);
    }
    this.total_price += this.shippingPrice;
    this.total_price -= this.voucherPrice;
  }
  getAllVouchers(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.voucherService.getAllVouchers().subscribe({
        next: (res) => {
          this.vouchers = res;
          resolve();
        },
        error: (error) => reject(error)
      });
    });
  }
  getAllPayments(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.paymentService.getAllPayments().subscribe({
        next: (res) => {
          this.payments = res;
          resolve();
        },
        error: (error) => reject(error)
      });
    });
  }
  getUserById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.userService.getUserById(id).subscribe({
        next: (res) => {
          this.user = res;
          console.log(this.user);
          resolve();
        },
        error: (error) => reject(error)
      });
    });
  }
  setVoucher() {
    this.voucherId = -1;
    this.voucherPrice = 0;
    let isExist = false;
    let voucher = this.inputVoucher.trim();
    for (let i = 0; i < this.vouchers.length; i++) {
      if (this.vouchers[i].voucherName === voucher) {
        isExist = true;
        let now = new Date();
        if (this.vouchers[i].status !== 0) {
          alert('Voucher không còn hoạt động');
          return;
        }
        if (now < this.vouchers[i].dateTo || now > this.vouchers[i].dateFrom) {
          alert('Voucher đã hết hạn');
          return;
        }
        this.voucherId = this.vouchers[i].id;
        if (Number(this.vouchers[i].unit) === 0) { // %
          this.calculateTotalPriceForVoucherStatus0();
          this.voucherPrice = this.vouchers[i].discount / 100 * this.total_price;
        } else {
          this.voucherPrice = this.vouchers[i].discount;
        }
        this.calculateTotalPrice();
        return;
      }
    }
    if (!isExist) {
      alert('Voucher không tồn tại');
    }
  }
  calculateTotalPriceForVoucherStatus0() {
    this.total_price = 0;
    for (let i = 0; i < this.products.length; i++) {
      this.total_price += this.products[i].quantity * ((1 - this.products[i].product.discount) * this.products[i].product.priceToSell);
    }
    this.total_price += this.shippingPrice;
  }
  addOrder(orderData: Order): Promise<Order> {
    return new Promise((resolve, reject) => {
      this.orderService.addOrder(orderData).subscribe({
        next: (res) => {
          resolve(res);
        },
        error: (error) => reject(error)
      })
    })
  }
  addOrderline(orderlineData: Orderline): Promise<void> {
    return new Promise((resolve, reject) => {
      this.orderlineService.addOrderline(orderlineData).subscribe({
        next: (res) => {
          resolve();
        },
        error: (error) => reject(error)
      })
    })
  }
  deleteCartByUserid(userId: number): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.cartService.deleteCartByUserid(userId).subscribe({
        next: (res: boolean) => {
          resolve(res); // Trả về `true` nếu thành công, `false` nếu không thành công
        },
        error: (error) => reject(error)
      });
    });
  }
  async onThanhToan() {
    let order_date = new Date();
    let shipping_fee = this.shippingPrice;
    let order_discount = this.voucherPrice;
    let order_total = this.total_price;
    let address = this.user.address;
    let status = 2;
    let code = "DH-00";
    let userid = this.userId;
    let paymentId = 1;
    let orderData: Order = {
      orderDate: order_date,
      shippingFee: shipping_fee,
      orderDiscount: order_discount,
      orderTotal: order_total,
      code:code,
      address: address,
      status: status,
      userId: userid,
      paymentId: paymentId,
    }
    if (this.voucherId !== -1) {
      orderData.voucherId = this.voucherId;
    }
    console.log(this.products);
    
    let order: Order = await this.addOrder(orderData);

    let orderid = order.id;
    for (let i = 0; i < this.products.length; i++) {
      try {
        let orderlineData: Orderline = {
          orderId: Number(orderid),
          productId: this.products[i].product.id,
          quantity: this.products[i].quantity,
          unitPrice: (1 - this.products[i].product.discount) * this.products[i].product.priceToSell
        }
        console.log(orderlineData);
        await this.addOrderline(orderlineData);
      } catch (error) {
        console.error(error);
      }
    }

    if (this.fromCart) {  
      let isDeleted = await this.deleteCartByUserid(this.userId);
      if (!isDeleted) {
        alert('Không thể xóa giỏ hàng. Vui lòng thử lại.');
        return;
      }
    }
    alert('Tạo hóa đơn thành công');
    this.router.navigate(['/orderDetail'], { queryParams: { orderId: order.id } });
  }
}
