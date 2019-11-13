import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ReglementService {
  private headers: HttpHeaders;
  private accessPointUrl = 'http://localhost:59948/api/reglement';
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
   }
   public getAll() {
    // Get all jogging data
    return this.http.get(this.accessPointUrl, {headers: this.headers});
  }
  public getByFactureId(id) {
    return this.http.get(this.accessPointUrl + '/factureTrituration/' + id, {headers: this.headers});
  }
  public add(form: any) {
    return this.http.post(this.accessPointUrl, form, {headers: this.headers});
  }
}
