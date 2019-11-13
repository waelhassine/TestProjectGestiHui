import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class StockageHuileService {
  private headers: HttpHeaders;
  private accessPointUrl = 'http://localhost:59948/api/stockhuile';
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
   }
   public getAll() {
    // Get all jogging data
    return this.http.get(this.accessPointUrl, {headers: this.headers});
  }
}
