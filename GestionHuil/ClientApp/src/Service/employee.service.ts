import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private headers: HttpHeaders;
  private accessPointUrl = 'http://localhost:59948/api/employee';
  private acessPointAddUrl = 'http://localhost:59948/api/employee/register';
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
   }
   public get() {
    // Get all jogging data
    return this.http.get(this.accessPointUrl, {headers: this.headers});
  }
  public getId(id) {
    return this.http.get(this.accessPointUrl + '/' + id, {headers: this.headers});
  }
  public add(coordonnes: any) {
    return this.http.post(this.acessPointAddUrl, coordonnes, {headers: this.headers});
  }
  public remove(id) {
    return this.http.delete(this.accessPointUrl + '/' + id, {headers: this.headers});
  }

  public update(id, form) {
    return this.http.put(this.accessPointUrl + '/' + id, form, {headers: this.headers});
  }
}
