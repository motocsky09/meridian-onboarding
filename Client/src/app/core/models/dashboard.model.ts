export interface EmployeeSummary {
  id: string;
  firstName: string;
  lastName: string;
  role: string;
  department: string;
  startDate: string;
}

export interface Person {
  id: string;
  fullName: string;
  role: string;
}

export interface ChecklistItem {
  id: string;
  title: string;
  description?: string;
  completed: boolean;
}

export interface ScheduleEvent {
  id: string;
  title: string;
  type: string;
  date: string;
  withPersonName?: string;
}

export interface Dashboard {
  employee: EmployeeSummary;
  manager?: Person;
  buddy?: Person;
  checklistByStage: { [stage: string]: ChecklistItem[] };
  schedule: ScheduleEvent[];
}
