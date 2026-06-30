import { Routes } from '@angular/router';
import { OnboardingDashboardComponent } from './features/onboarding-dashboard/onboarding-dashboard.component';
import { TeamDirectoryComponent } from './features/team-directory/team-directory.component';

export const routes: Routes = [
  { path: '', component: OnboardingDashboardComponent },
  { path: 'team', component: TeamDirectoryComponent },
  { path: '**', redirectTo: '' }
];
