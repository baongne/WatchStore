import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    private apiUrl = 'https://localhost:7084/api/Product';

    constructor(private http: HttpClient) { }

    getAllProducts(): Observable<any> {
        return this.http.get(`${this.apiUrl}`);
    }
    getProductById(id: string): Observable<any> { 
        return this.http.get(`${this.apiUrl}/${id}`);
    }
}
