import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { Order } from '../../models/order.model';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../services/order.service';
import { OrderlineService } from '../../services/orderline.service';
import { Orderline } from '../../models/orderline.model';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, SidebarComponent, CommonModule, FormsModule],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent implements OnInit {
  userId !: number;
  orders: Order[] = [];
  ordersTemp: Order[] = [];
  ordersShow: Order[] = [];
  showOverlay: boolean = false;
  selectedOrder: Orderline[] = [];
  filterObj = {
    'PageNumber': 1,
    'PageSize': 10,
    'dateFrom': new Date(new Date().setDate(new Date().getDate() - 365)).toISOString().substring(0, 10),
    'dateNow': new Date().toISOString().substring(0, 10)
  };

  totalPages = 0;

  // TÍNH TỔNG SỐ TRANG

  calculateTotalPages() {
    this.totalPages = Math.ceil(this.ordersTemp.length / this.filterObj.PageSize);
  }
  // PHÂN TRANG
  setDataPageNumber(page: number) {
    let start = (page - 1) * this.filterObj.PageSize;
    let end = page * this.filterObj.PageSize;
    this.ordersShow = this.ordersTemp.slice(start, end);
  }

  constructor(private orderService: OrderService, private orderlineService: OrderlineService
    , private productService: ProductService, private router:Router
  ) { }
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
  getAllOrdes(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.orderService.getAllOrders().subscribe({
        next: (res) => {
          let allOrders: Order[] = res;
          for (let i = 0; i < allOrders.length; i++) {
            if (allOrders[i].userId === this.userId) {
              this.orders.push(allOrders[i]);
            }
          }
          this.ordersTemp = this.orders;
          this.calculateTotalPages();
          // SET PRODUCTS SHOW
          this.setDataPageNumber(this.filterObj.PageNumber);
          console.log(this.orders);
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
              this.selectedOrder.push(allOrderLine[i]);
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
  ngOnInit(): void {
    this.setMaxDate();
    this.userId = Number(this.getCookie('id')) || -1;
    this.getAllOrdes();
  }
  async viewDetails(orderid: number) {
    this.selectedOrder = [];
    await this.getAllOrderLine(orderid);
    for (let i = 0; i < this.selectedOrder.length; i++) {
      this.selectedOrder[i].product = await this.getProductById(this.selectedOrder[i].productId);
    }
    console.log(this.selectedOrder);
    //this.selectedOrder = order;
    this.showOverlay = true; // Đảm bảo biến này được đặt thành true khi nhấn vào nút
  }

  closeOverlay() {
    this.showOverlay = false; // Đóng overlay khi nhấn nút X
  }
  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'Đã duyệt';
      case 1: return 'Từ chối';
      case 2: return 'Chờ duyệt';
      default: return '';
    }
  }
  // FORMAT VND
  formatCurrencyVND(amount: number): string {
    return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }
  onPrevious() {
    if (Number(this.filterObj.PageNumber) === 1) {
      this.filterObj.PageNumber = 1;
    } else {
      this.filterObj.PageNumber = Number(this.filterObj.PageNumber) - 1;
    }
    this.setDataPageNumber(this.filterObj.PageNumber);
  }
  onNext() {
    if (Number(this.filterObj.PageNumber) === this.totalPages) {
      this.filterObj.PageNumber = this.totalPages;
    } else {
      this.filterObj.PageNumber = Number(this.filterObj.PageNumber) + 1;
    }
    this.setDataPageNumber(this.filterObj.PageNumber);
  }
  executeFilter() {
    if (this.filterObj.dateFrom > this.filterObj.dateNow) {
      alert('Ngày bắt đầu phải nhỏ hơn ngày kết thúc.');
      const tempDate = new Date(this.filterObj.dateNow);
      tempDate.setDate(tempDate.getDate() - 1);
      this.filterObj.dateFrom = tempDate.toISOString();
      return;
    }

    this.filterObj.PageNumber = 1;

    // Chuyển dateFrom và dateNow thành ngày mà không có giờ, phút, giây
    const dateFrom = this.removeTime(new Date(this.filterObj.dateFrom));
    const dateNow = this.removeTime(new Date(this.filterObj.dateNow));

    // Lọc các đơn hàng
    this.ordersTemp = this.orders.filter(order => {
      const orderDate = this.removeTime(new Date(order.orderDate));
      return orderDate >= dateFrom && orderDate <= dateNow;
    });

    this.calculateTotalPages();
    this.setDataPageNumber(this.filterObj.PageNumber);
  }

  // Hàm loại bỏ giờ, phút, giây và mili giây
  removeTime(date: Date): Date {
    date.setHours(0, 0, 0, 0); // Đặt giờ, phút, giây và mili giây về 0
    return date;
  }

  convertDateToDMY(dateString: string | Date): string {
    const date = new Date(dateString);  // Tạo đối tượng Date từ chuỗi hoặc Date

    const day = ("0" + date.getDate()).slice(-2);
    const month = ("0" + (date.getMonth() + 1)).slice(-2);
    const year = date.getFullYear();

    return `${day}-${month}-${year}`;
  }
  maxDate: string = '';
  setMaxDate(): void {
    // Thiết lập maxDate là ngày hiện tại (tính theo định dạng YYYY-MM-DD)
    const today = new Date();
    this.maxDate = today.toISOString().split('T')[0];
  }
  viewInvoice(orderId: number) {
    this.router.navigate(['/orderDetail'], { queryParams: { orderId: orderId } });
}
}
