import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../../shared/footer/footer.component';
import { HeaderComponent } from '../../shared/header/header.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { OrderService } from '../../services/order.service';
import { OrderlineService } from '../../services/orderline.service';
import { ActivatedRoute } from '@angular/router';
import { Order } from '../../models/order.model';
import { Orderline } from '../../models/orderline.model';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-invoice',
  standalone: true,
  imports: [FooterComponent, HeaderComponent, SidebarComponent, CommonModule],
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css'
})
export class InvoiceComponent implements OnInit {
  user: User = {
    address: '',
    email: '',
    name: '',
    password: '',
    phone: '',
    role: '',
    status: '',
    id: '',
  };
  userId!: number;
  orderId: number = -1;
  order: Order = {
    code: 'DH-00',
    address: '',
    orderDate: new Date,
    orderDiscount: -1,
    orderTotal: -1,
    paymentId: -1,
    shippingFee: -1,
    status: -1,
    userId: -1,
    id: -1,
  };
  orderLine: Orderline[] = [];
  subTotal: number = 0;
  constructor(private userService: UserService, private orderService: OrderService, private orderlineService: OrderlineService
    , private route: ActivatedRoute, private productService: ProductService

  ) { }
  getUserById(id: string): Promise<void> {
    return new Promise((resolve, reject) => {
      this.userService.getUserById(id).subscribe({
        next: (res) => {
          this.user = res;
          resolve();
        }
      })
    })
  }
  ngOnInit(): void {
    this.userId = Number(this.getCookie('id')) || -1;
    this.route.queryParams.subscribe(params => {
      this.orderId = params['orderId'];
    });
    this.getUserById(this.userId.toString());
    this.executeOrder();
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
  getAllOrders(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.orderService.getAllOrders().subscribe({
        next: (res) => {
          let allOrders: Order[] = res;
          for (let i = 0; i < allOrders.length; i++) {
            if (allOrders[i].id === Number(this.orderId)) {
              this.order = allOrders[i];
              break;
            }
          }

          resolve();
        }
      })
    })
  }
  getAllOrderLine(orderId: number): Promise<void> {
    return new Promise((resolve, reject) => {
      this.orderlineService.getAllOrderline().subscribe({
        next: (res) => {
          let allOrderLine: Orderline[] = res;
          for (let i = 0; i < allOrderLine.length; i++) {
            if (allOrderLine[i].orderId == orderId) {
              this.orderLine.push(allOrderLine[i]);
            }
          }
          resolve();
        }
      })
    })
  }
  getProductById(productId: number): Promise<Product> {
    return new Promise((resolve, reject) => {
      this.productService.getProductById(productId.toString()).subscribe({
        next: (res) => {
          resolve(res);
        }
      })
    })
  }
  async executeOrder() {
    await this.getAllOrders();
    await this.getAllOrderLine(this.orderId);
    console.log(this.orderLine);
    for (let i = 0; i < this.orderLine.length; i++) {
      this.orderLine[i].product = await this.getProductById(this.orderLine[i].productId);
    }
    this.calculateSubTotal(this.orderLine);
  }
  convertDateToDMY(dateString: string | Date): string {
    const date = new Date(dateString);  // Tạo đối tượng Date từ chuỗi hoặc Date

    const day = ("0" + date.getDate()).slice(-2);
    const month = ("0" + (date.getMonth() + 1)).slice(-2);
    const year = date.getFullYear();

    return `${day}-${month}-${year}`;
  }
  // FORMAT VND
  formatCurrencyVND(amount: number): string {
    return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }
  calculateSubTotal(orderline: Orderline[]) {
    for(let i = 0 ; i< orderline.length ; i++){
      this.subTotal+=orderline[i].quantity*orderline[i].unitPrice;
    }
  }
}
