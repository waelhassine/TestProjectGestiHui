import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class StatistiqueService {
  private headers: HttpHeaders;
  private accessPointUrl = 'http://localhost:59948/api/statistique';
  private data = {};
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
   }
   public getAll() {
    // Get all jogging data
    return this.http.get(this.accessPointUrl, {headers: this.headers});
  }
  public getByWeek(form: any) {
    return this.http.post(this.accessPointUrl, form, {headers: this.headers});
  }

}
