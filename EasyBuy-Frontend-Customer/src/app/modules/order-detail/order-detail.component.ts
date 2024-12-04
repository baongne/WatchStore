import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { OrderService } from '../../services/order.service';
import { OrderlineService } from '../../services/orderline.service';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { ActivatedRoute, Route, Router, RouterModule } from '@angular/router';
import { Orderline } from '../../models/orderline.model';
import { Order } from '../../models/order.model';
import { User } from '../../models/user.model';
import { Product } from '../../models/product.model';
import { Vnpay } from '../../models/vnpay.model';
import { VNpayService } from '../../services/vnpay.service';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule,HeaderComponent,FooterComponent,SidebarComponent,RouterModule],
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.css'
})
export class OrderDetailComponent implements OnInit{

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
    , private route: ActivatedRoute, private productService: ProductService, private vnpayService:VNpayService,  private router:Router

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

  ngOnInit(): void {
    this.userId = Number(this.getCookie('id')) || -1;
    this.route.queryParams.subscribe(params => {
      this.orderId = params['orderId'];
    });
    this.getUserById(this.userId.toString());
    this.executeOrder();

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
  getStatusText(status: number): string {
    switch (status) {
      case 0:
        return 'Đã duyệt';
      case 1:
        return 'Từ chối';
      case 2:
        return 'Đang chờ';
      default:
        return 'Không xác định';
    }
  }
  async goToVnpay(){
    let vnpayForm:Vnpay;
    vnpayForm={
      orderType: "other",
      amount: this.order.orderTotal,
      orderDescription: "Thanh toán hóa đơn",
      name:this.user.name,
      orderId:this.order.id
    }

    let url = await this.callVnpay(vnpayForm);
   // this.router.navigate([`/${url}`]);


  }

  callVnpay(form: Vnpay): Promise<any> {
    return new Promise((resolve, reject) => {
      this.vnpayService.callVnpay(form).subscribe({
        next: (res) => {
          const paymentUrl = res.url;  
          window.location.href = paymentUrl;  
          resolve(res.Url);
        },
        error: (err) => {
          console.error(err);
          reject(err);
        }
      });
    });
  }
  



}
