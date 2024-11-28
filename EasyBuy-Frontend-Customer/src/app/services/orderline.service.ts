import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order.model';
import { Orderline } from '../models/orderline.model';

@Injectable({
    providedIn: 'root'
})
export class OrderlineService {

    private apiUrl = 'https://localhost:7084/api/Orderline';

    constructor(private http: HttpClient) { }

    addOrderline(orderlineData: Orderline): Observable<any> {
        return this.http.post(`${this.apiUrl}`, orderlineData);
    }
    getAllOrderline(): Observable<Orderline[]> { 
        return this.http.get<Orderline[]>(`${this.apiUrl}`);
    }
}
