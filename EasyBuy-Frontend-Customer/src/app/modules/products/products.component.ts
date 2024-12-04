import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../../shared/header/header.component';
import { FooterComponent } from '../../shared/footer/footer.component';
import { SidebarComponent } from '../../shared/sidebar/sidebar.component';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { firstValueFrom, min } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-products',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, SidebarComponent, CommonModule, FormsModule, RouterModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  categories: Category[] = [];
  products: Product[] = [];
  productsShow: Product[] = [];
  minPrice = 0;
  maxPrice = 50000000;
  filterObj = {
    'PageNumber': 1,
    'PageSize': 12,
    'Gender': 'all',
    'Sort': 'all',
    'categoryId': 0,
    'priceMin': this.minPrice,
    'priceMax': this.maxPrice
  };
  totalPages = 0;
  // TÍNH TỔNG SỐ TRANG
  calculateTotalPages() {
    this.totalPages = Math.ceil(this.products.length / this.filterObj.PageSize);
  }
  // PHÂN TRANG
  setDataPageNumber(page: number) {
    let start = (page - 1) * this.filterObj.PageSize;
    let end = page * this.filterObj.PageSize;
    this.productsShow = this.products.slice(start, end);
  }


  // CONSTRUCTOR

  constructor(private categoryService: CategoryService, private productService: ProductService
    , private router: Router, private route: ActivatedRoute
  ) { }
  // KHỞI TẠO

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.filterObj.PageNumber = params['page'] || 1;
      this.filterObj.categoryId = params['categoryid'] || 0;
      this.filterObj.Gender = params['gender'] || 'all';
      this.filterObj.Sort = params['sort'] || 'all';
      this.filterObj.priceMin = params['min'] || this.minPrice;
      this.filterObj.priceMax = params['max'] || this.maxPrice;

    });
    this.getAllCategories();
    this.getAllProducts();
  }
  // GET ALL CATEGORIES
  async getAllCategories() {
    let cate: Category[] = [];
    try {
      cate = await firstValueFrom(this.categoryService.getAllCategories());
      for (let i = 0; i < cate.length; i++) {
        if (cate[i].status === 0) {
          this.categories.push(cate[i]);
        }
      }
    } catch (error) {
      console.error('Lỗi khi gọi API:', error);
    }
  }
  // GET ALL PRODUCTS
  async getAllProducts() {
    let pro: Product[] = [];
    let pros: Product[] = []
    try {
      pro = await firstValueFrom(this.productService.getAllProducts());
      for (let i = 0; i < pro.length; i++) {
        if (pro[i].status === 1) {
          pros.push(pro[i]);
        }
      }
      this.products = pros;
      this.executeFilter();
      // TINH TONG TRANG 
      this.calculateTotalPages();
      // SET PRODUCTS SHOW
      this.setDataPageNumber(this.filterObj.PageNumber);
      
    } catch (error) {
      console.error('Lỗi khi gọi API:', error);
    }
  }
  // FORMAT VND
  formatCurrencyVND(amount: number): string {
    return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }

  sortProducts(products: Product[], sortBy: string) {

    return products.sort((a, b) => {
      const aFinalPrice = a.priceToSell - a.priceToSell * a.discount;
      const bFinalPrice = b.priceToSell - b.priceToSell * b.discount;
      if (sortBy === 'thaptoicao') {
        return aFinalPrice - bFinalPrice;
      } else if (sortBy === 'caovethap') {
        return bFinalPrice - aFinalPrice;
      } else {
        return 0;
      }
    });
  }

  updateFilters() {
    this.filterObj.PageNumber = 1;
    if (this.filterObj.priceMin > this.filterObj.priceMax) {
      this.filterObj.priceMin = this.minPrice;
      this.filterObj.priceMax = this.maxPrice;
    }
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        categoryid: this.filterObj.categoryId !== 0 ? this.filterObj.categoryId : null,
        page: this.filterObj.PageNumber !== 1 ? this.filterObj.PageNumber : null,
        gender: this.filterObj.Gender !== 'all' ? this.filterObj.Gender : null,
        sort: this.filterObj.Sort !== 'all' ? this.filterObj.Sort : null,
        min: this.filterObj.priceMin !== this.minPrice ? this.filterObj.priceMin : null,
        max: this.filterObj.priceMax !== this.maxPrice ? this.filterObj.priceMax : null,


      },
      queryParamsHandling: 'merge'
    });
    this.getAllProducts();
  }
  executeFilter() {
    let categoryid = Number(this.filterObj.categoryId);
    let gender = this.filterObj.Gender;
    let sort = this.filterObj.Sort;
    if (categoryid !== 0) {
      this.products = this.products.filter(p => p.categoryId === categoryid);
    }
    if (gender !== 'all') {
      this.products = this.products.filter(p => p.gender.toLowerCase() === gender.toLowerCase());
    }

    this.products = this.products.filter(p => {
      const priceAfterDiscount = (1 - p.discount) * p.priceToSell;
      return priceAfterDiscount >= this.filterObj.priceMin && priceAfterDiscount <= this.filterObj.priceMax;
    });
     this.sortProducts(this.products, sort);
  }
  updateFiltersPagination() {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        categoryid: this.filterObj.categoryId !== 0 ? this.filterObj.categoryId : null,
        page: this.filterObj.PageNumber !== 1 ? this.filterObj.PageNumber : null,
        gender: this.filterObj.Gender !== 'all' ? this.filterObj.Gender : null,
        sort: this.filterObj.Sort !== 'all' ? this.filterObj.Sort : null,
        min: this.filterObj.priceMin !== this.minPrice ? this.filterObj.priceMin : null,
        max: this.filterObj.priceMax !== this.maxPrice ? this.filterObj.priceMax : null,
      },
      queryParamsHandling: 'merge'
    });
    this.getAllProducts();
  }
  onPrevious() {
    if (Number(this.filterObj.PageNumber) === 1) {
      this.filterObj.PageNumber = 1;
    } else {
      this.filterObj.PageNumber = Number(this.filterObj.PageNumber) - 1;
    }
    this.updateFiltersPagination();
  }
  onNext() {
    if (Number(this.filterObj.PageNumber) === this.totalPages) {
      this.filterObj.PageNumber = this.totalPages;
    } else {
      this.filterObj.PageNumber = Number(this.filterObj.PageNumber) + 1;
    }
    this.updateFiltersPagination();

  }
}
