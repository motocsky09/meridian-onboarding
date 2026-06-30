import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Dashboard } from '../models/dashboard.model';
import { Employee } from '../models/employee.model';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private readonly baseUrl = 'http://localhost:5290/api';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.baseUrl}/employees`);
  }

  getById(id: string): Observable<Employee> {
    return this.http.get<Employee>(`${this.baseUrl}/employees/${id}`);
  }

  getDashboard(id: string): Observable<Dashboard> {
    return this.http.get<Dashboard>(`${this.baseUrl}/employees/${id}/dashboard`);
  }

  toggleChecklistItem(itemId: string): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/checklist/${itemId}/toggle`, {});
  }
}
