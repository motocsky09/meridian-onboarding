import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { EmployeeService } from '../../core/services/employee.service';
import { Employee } from '../../core/models/employee.model';

@Component({
  selector: 'app-team-directory',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './team-directory.component.html',
  styleUrls: ['./team-directory.component.css']
})
export class TeamDirectoryComponent implements OnInit {
  employees: Employee[] = [];
  loading = true;
  error = false;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.employeeService.getAll().subscribe({
      next: (data) => {
        this.employees = data;
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
    });
  }

  get departments(): string[] {
    const unique = new Set(this.employees.map((e) => e.department));
    return Array.from(unique).sort();
  }

  employeesInDepartment(department: string): Employee[] {
    return this.employees
      .filter((e) => e.department === department)
      .sort((a, b) => a.lastName.localeCompare(b.lastName));
  }
}
