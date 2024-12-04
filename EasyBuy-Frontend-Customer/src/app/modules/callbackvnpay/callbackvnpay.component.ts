import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-callbackvnpay',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './callbackvnpay.component.html',
  styleUrl: './callbackvnpay.component.css'
})
export class CallbackvnpayComponent implements OnInit {
  loading = true;
  success = false;
  message = '';

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    const queryParams = this.route.snapshot.queryParams;
    const orderId = this.route.snapshot.paramMap.get('id');
    console.log(orderId);
    this.http
      .get(`https://localhost:7084/api/Order/callback/vnpay/${orderId}`, { params: queryParams })
      .subscribe({
        next: (res: any) => {
          this.loading = false;
          if (res.success) {
            this.success = true;
            this.message = res.message;
            alert('Thanh toán thành công');
            this.goToHome();
          } else {
            this.success = false;
            this.message = res.message;
          }
        },
        error: () => {
          this.loading = false;
          this.success = false;
          this.message = 'Đã xảy ra lỗi khi xử lý giao dịch.';
        }
      });


  }

  goToHome(): void {
    this.router.navigate(['/home']);
  }


}
