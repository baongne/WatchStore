import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class VoucherService {

    private apiUrl = 'https://localhost:7084/api/Voucher';

    constructor(private http: HttpClient) { }

    getAllVouchers(): Observable<any> {
        return this.http.get(`${this.apiUrl}`);
    }
    
}
