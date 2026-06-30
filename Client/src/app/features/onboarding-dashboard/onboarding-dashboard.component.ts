import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../core/services/employee.service';
import { Dashboard, ChecklistItem } from '../../core/models/dashboard.model';

@Component({
  selector: 'app-onboarding-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './onboarding-dashboard.component.html',
  styleUrls: ['./onboarding-dashboard.component.css']
})
export class OnboardingDashboardComponent implements OnInit {
  dashboard: Dashboard | null = null;
  loading = true;
  error = false;

  // Hardcoded for MVP — Andrei Motoc's id from seed data.
  private readonly employeeId = 'd7fa1eb7-e2bd-4b8d-bea3-87d346011546';

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.employeeService.getDashboard(this.employeeId).subscribe({
      next: (data) => {
        this.dashboard = data;
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
    });
  }

  get checklistStages(): string[] {
    return this.dashboard ? Object.keys(this.dashboard.checklistByStage) : [];
  }

  toggleItem(item: ChecklistItem): void {
    const previousState = item.completed;
    item.completed = !item.completed; // optimistic update

    this.employeeService.toggleChecklistItem(item.id).subscribe({
      error: () => {
        item.completed = previousState; // revert on failure
      }
    });
  }
}
