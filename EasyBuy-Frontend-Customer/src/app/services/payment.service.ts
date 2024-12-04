import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class PaymentService {

    private apiUrl = 'https://localhost:7084/api/Payment';

    constructor(private http: HttpClient) { }

    getAllPayments(): Observable<any> {
        return this.http.get(`${this.apiUrl}`);
    }
    
}
