import { Routes } from '@angular/router';
import { OnboardingDashboardComponent } from './features/onboarding-dashboard/onboarding-dashboard.component';

export const routes: Routes = [
  { path: '', component: OnboardingDashboardComponent },
  { path: '**', redirectTo: '' }
];
