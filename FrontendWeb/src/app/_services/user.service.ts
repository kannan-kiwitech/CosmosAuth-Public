import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'https://localhost:5001/api/';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' } )
};

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  profile(): Observable<any> {
    return this.http.get(API_URL + 'user', httpOptions);
  }
  
}
