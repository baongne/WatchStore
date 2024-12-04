import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vnpay } from '../models/vnpay.model';

@Injectable({
    providedIn: 'root'
})
export class VNpayService {

    private apiUrl = 'https://localhost:7084/api/Payment';

    constructor(private http: HttpClient) { }

    callVnpay(form: Vnpay): Observable<any> {
        return this.http.post(`${this.apiUrl}/create-url`, form);
    }
    
}
