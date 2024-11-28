import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent implements OnInit, OnDestroy {
  constructor(private authService: AuthService, private router: Router) { }
  name: string = '';
  role: string = '';
  id !: string;
  ngOnInit(): void {

    if (this.isLogin()) {
      this.name = this.getCookie('name') ?? '';
      this.role = this.getCookie('role') ?? '';
      this.id = this.getCookie('id') ?? '';
    }
   

  }

  ngOnDestroy(): void {
    
  }


  isLogin(): boolean {
    if (!this.checkCookie('id')) {
      this.router.navigate(['/dangnhap']);
      return false;
    }
    return true;
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


  logoutBtn() {
    this.clearCookies();
  }
}
