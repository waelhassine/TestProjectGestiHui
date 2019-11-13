import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class FactureService {
  private headers: HttpHeaders;
  private accessPointUrl = 'http://localhost:59948/api/facture';
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
public getByIdClient(id) {
  return this.http.get(this.accessPointUrl + '/client/' + id, {headers: this.headers});
}
public geTriturationtByIdClient(id) {
  return this.http.get(this.accessPointUrl + '/factureclient/' + id, {headers: this.headers});
}
public geAchatByIdClient(id) {
  return this.http.get(this.accessPointUrl + '/achat/' + id, {headers: this.headers});
}
public geVenteByIdClient(id) {
  return this.http.get(this.accessPointUrl + '/vente/' + id, {headers: this.headers});
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
}
