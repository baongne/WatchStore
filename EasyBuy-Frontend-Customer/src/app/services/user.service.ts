import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private apiUrl = 'https://localhost:7084/api/User';

    constructor(private http: HttpClient) { }

    getUserById(id: string): Observable<any> {
        return this.http.get(`${this.apiUrl}/${id}`);
    }
    updateUser(id: string, userData: any): Observable<any> {
        return this.http.put(`${this.apiUrl}/${id}`, userData);
    }

}
