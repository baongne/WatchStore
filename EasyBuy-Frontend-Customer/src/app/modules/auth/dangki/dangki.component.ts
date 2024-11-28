import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User,Users } from '../../../models/user.model';
import { firstValueFrom } from 'rxjs';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-dangki',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './dangki.component.html',
  styleUrl: './dangki.component.css'
})
export class DangkiComponent implements OnInit {
  form !: FormGroup;

  constructor(private router: Router, private userService: UserService, private fb: FormBuilder, private authService: AuthService) {
    this.form = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      address: ['', [Validators.required, Validators.minLength(6)]],
      phone: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    }, { validators: this.passwordMatchValidator });
  }
  passwordMatchValidator(form: FormGroup) {
    return form.get('password')?.value === form.get('confirmPassword')?.value
      ? null : { 'mismatch': true };
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
      return [];
    }

  }
  async onSubmit() {
    let emailExist = false;
    let email = this.form.get('email')?.value;
    let users = await this.getAllUsers();
    for (let i = 0; i < users.length; i++) {
      if (users[i].email === email) {
        emailExist = true;
        break;
      }
    }
    if (emailExist) {
      alert('Email đã tồn tại');
      return;
    }
    let userData: Users = {
      id: 0,
      name: this.form.get('name')?.value,
      email: this.form.get('email')?.value,
      phone: this.form.get('phone')?.value,
      password: this.form.get('password')?.value,
      address: this.form.get('address')?.value,
      status: 0,
      role: 0,
    }
    this.authService.addUser(userData).subscribe({
      next: (res) => {
        alert("Đăng kí thành công");
        console.log(res);
        this.router.navigate(['/dangnhap']);
      },
      error: (error) => {
        console.error('Có lỗi:', error);
      }
    })
  }
}
