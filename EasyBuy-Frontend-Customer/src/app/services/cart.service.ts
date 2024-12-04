import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart.model';

@Injectable({
    providedIn: 'root'
})
export class CartService {

    private apiUrl = 'https://localhost:7084/api/Cart';

    constructor(private http: HttpClient) { }

    addCart(cartData: Cart): Observable<any> {
        return this.http.post(`${this.apiUrl}`, cartData);
    }
    getAllCarts(): Observable<any> {
        return this.http.get(`${this.apiUrl}`);
    }
    updateCart(cartData: Cart, idCart: number): Observable<any> {
        return this.http.put(`${this.apiUrl}/${idCart}`, cartData);
    }
    deleteCart(cartId: number): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${cartId}`);
    }
    deleteCartByUserid(userId: number): Observable<boolean> {
        return this.http.delete<boolean>(`${this.apiUrl}/DeleteCart/${userId}`);
      }
      
}
