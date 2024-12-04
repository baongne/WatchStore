import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order.model';

@Injectable({
    providedIn: 'root'
})
export class OrderService {

    private apiUrl = 'https://localhost:7084/api/Order';
    
    constructor(private http: HttpClient) { }

    addOrder(orderData: Order): Observable<Order> {
        return this.http.post<Order>(`${this.apiUrl}/AddOrder`, orderData);
    }
    getAllOrders():Observable<Order[]>{
        return this.http.get<Order[]>(`${this.apiUrl}`);
    }
    getOrderById(id:number):Observable<Order>{
        return this.http.get<Order>(`${this.apiUrl}/`+id);
    }
}
