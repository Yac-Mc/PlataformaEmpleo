import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

// Modulos
import { SharedModule } from '../shared/shared.module';

import { CommonModule } from '@angular/common';
import { PagesComponent } from './pages.component';
import { ProgressComponent } from './progress/progress.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    PagesComponent,
    ProgressComponent,
    DashboardComponent
  ],
  exports:[
    PagesComponent,
    ProgressComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule
  ]
})
export class PagesModule { }
