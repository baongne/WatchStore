import { CommonModule } from '@angular/common';
import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit, OnDestroy {
  name: string = '';
  role: string = '';
  id !: string;

  constructor(private router: Router, private authService: AuthService) { }

  isDropdownOpen = false;

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  closeDropdown(): void {
    this.isDropdownOpen = false;
  }



  ngOnInit(): void {
    if (this.isLogin()) {
      this.name = this.getCookie('name') ?? ''; 
      this.role = this.getCookie('role') ?? ''; 
      this.id  = this.getCookie('id') ?? '';
    }/*
    // Thêm sự kiện beforeunload để xóa cookie khi người dùng đóng tab
    if (!this.checkCookie('remember')) {
      window.addEventListener('beforeunload', this.clearCookies.bind(this));
    }*/
  }

  ngOnDestroy(): void {
    /*
    // Gỡ bỏ sự kiện khi component bị hủy
    if (!this.checkCookie('remember')) {
      window.removeEventListener('beforeunload', this.clearCookies.bind(this));
    }
    */
  }
  isLogin(): boolean {
    if (!this.checkCookie('id')) {
      this.router.navigate(['/dangnhap']);
      return false;
    }
    return true;
  }

  checkCookie(name: string): boolean {
    const nameEQ = `${name}=`;
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i].trim();
      if (c.indexOf(nameEQ) === 0) {
        return true;
      }
    }
    return false;
  }
  deleteCookie(name: string) {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  }
  clearCookies() {

    this.deleteCookie('id');
    this.deleteCookie('name');
    this.deleteCookie('role');
    if (this.checkCookie('remember')) {
      this.deleteCookie('remember');
    }

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



  logout() {
    this.clearCookies();
    this.router.navigate(['/dangnhap']);
  }

  searchKey: string = '';
  onSearch() {
    if (this.searchKey.trim() !== '') {  
      this.router.navigate(['/search-page', this.searchKey]);  
    }
  }
}
