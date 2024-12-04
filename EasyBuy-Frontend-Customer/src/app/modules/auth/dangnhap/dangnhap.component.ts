import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-dangnhap',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './dangnhap.component.html',
  styleUrl: './dangnhap.component.css'
})
export class DangnhapComponent implements OnInit {
  form !: FormGroup;
  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rememberMe: [false]
    });
  }
  ngOnInit(): void {

  }
  async getAllUsers() {
    let users = null;
    try {
      users = await firstValueFrom(this.authService.getAllUsers());
      return users;
    } catch (error) {
      console.error('Lỗi khi gọi API:', error);
    }

  }
  async onSubmit(): Promise<void> {
    let check = false;
    const users = await this.getAllUsers();
    const email = this.form.get('email')?.value;
    const password = this.form.get('password')?.value;
    const rememberMe = this.form.get('rememberMe')?.value;

    for (let i = 0; i < users.length; i++) {
      if (email === users[i].email) {
        check = true;
        if (users[i].status === 1) {
          alert('Tài khoản không còn hoạt động');
          return;
        }
        if (users[i].role === 1) {
          alert('Tài khoản không có quyền để truy cập');
          return;
        }
        if (users[i].password === password) {
          this.createCookie('id', users[i].id, 1000);
          this.createCookie('name', users[i].name, 1000);
          this.createCookie('role', users[i].role, 1000);
          if (rememberMe) {
            this.createCookie("remember", "true", 1000);
          }
          this.router.navigate(['/home']);
          return;
        } else {
          alert('Password sai');
          return;
        }
      }
    }

    if (!check) {
      alert('Email không tồn tại');
    }
  }
  createCookie(name: string, value: string, days: number) {
    const expires = new Date();
    expires.setTime(expires.getTime() + (days * 24 * 60 * 60 * 1000));
    const expiresString = `expires=${expires.toUTCString()}`;
    document.cookie = `${name}=${value};${expiresString};path=/`;
  }

}
