import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from '../../models/user.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, HeaderComponent, FooterComponent, SidebarComponent, ReactiveFormsModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  user!: User;
  userId !: string;
  form !: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      address: ['', [Validators.required, Validators.minLength(6)]],
      phone: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
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
  async ngOnInit(): Promise<void> {
    this.userId = this.getCookie('id') || '';
    await this.getUserById();
    this.khoiTaoForm();
  }
  async getUserById() {
    try {
      this.user = await firstValueFrom(this.userService.getUserById(this.userId));
      console.log(this.user);
    } catch (error) {
      console.error('Lỗi khi gọi API:', error);
    }
  }
  public onSubmit() {
    let userData: User = {
      id: this.user.id,
      name: this.form.get('name')?.value,
      email: this.form.get('email')?.value,
      phone: this.form.get('phone')?.value,
      password: this.form.get('password')?.value,
      address: this.form.get('address')?.value,
      status: this.user.status,
      role: this.user.role,
    }
    this.userService.updateUser(this.userId, userData).subscribe({
      next: (res) => {
        alert("Cập nhật thông tin thành công");
        console.log(res);
      },
      error: (error) => {
        console.error('Có lỗi:', error);
      }
    })
  }
  khoiTaoForm() {
    this.form = this.fb.group({
      name: [this.user.name, [Validators.required]],
      email: [this.user.email, [Validators.required, Validators.email]],
      password: [this.user.password, [Validators.required, Validators.minLength(6)]],
      address: [this.user.address, [Validators.required, Validators.minLength(6)]],
      phone: [this.user.phone, [Validators.required, Validators.pattern(/^\d+$/)]],
    });
  }
}
