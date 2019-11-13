import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class StockageOlivesService {
  private headers: HttpHeaders;
  private accessPointUrl = 'http://localhost:59948/api/stockageolive';
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
   }
   public getAll() {
    // Get all jogging data
    return this.http.get(this.accessPointUrl, {headers: this.headers});
  }
  public getById(id) {
    return this.http.get(this.accessPointUrl + '/' + id, {headers: this.headers});
  }
  public add(form: any) {
    return this.http.post(this.accessPointUrl, form, {headers: this.headers});
  }
  public remove(id) {
    return this.http.delete(this.accessPointUrl + '/' + id, {headers: this.headers});
  }

  public update(form) {
    return this.http.put(this.accessPointUrl + '/' + form.id, form, {headers: this.headers});
  }
  public filter(form) {
    return this.http.put(this.accessPointUrl + '/search' , form, {headers: this.headers});
  }
  public pdfDown(id): Observable<Blob> {

    return this.http.get(this.accessPointUrl + '/pdfcreator/' + id, {headers: this.headers, responseType: 'blob' });
   }
   public getAllvarite() {
    return this.http.get(this.accessPointUrl + '/' + 'variete' , {headers: this.headers});
   }
   public checkStockageExitsTrituration(id) {
    return this.http.get(this.accessPointUrl + '/check/' + id , {headers: this.headers});
   }

}
