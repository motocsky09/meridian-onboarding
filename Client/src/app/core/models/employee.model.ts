export interface Employee {
  id: string;
  firstName: string;
  lastName: string;
  role: string;
  department: string;
  startDate: string;
  managerId?: string;
  buddyId?: string;
}
