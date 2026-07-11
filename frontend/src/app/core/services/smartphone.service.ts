import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Smartphone } from '../models/smartphone.model';

@Injectable({
  providedIn: 'root'
})
export class SmartphoneService {
  private readonly apiUrl = '/api/smartphone';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Smartphone[]> {
    return this.http.get<Smartphone[]>(this.apiUrl);
  }

  getById(id: string): Observable<Smartphone> {
    return this.http.get<Smartphone>(`${this.apiUrl}/${id}`);
  }

  create(smartphone: Smartphone): Observable<string> {
    return this.http.post<string>(this.apiUrl, smartphone);
  }

  update(id: string, smartphone: Smartphone): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, smartphone);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}