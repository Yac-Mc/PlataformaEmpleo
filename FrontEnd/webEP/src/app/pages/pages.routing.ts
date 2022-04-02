import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { AuthGuard } from "../guards/auth.guard";

import { DashboardComponent } from "./dashboard/dashboard.component";
import { PagesComponent } from "./pages.component";
import { ProgressComponent } from "./progress/progress.component";

const routes: Routes = [
    {
        path: 'dashboard', component: PagesComponent,
        canActivate: [AuthGuard],
        children:[
            {path: '', component: DashboardComponent},
            {path: 'progress', component: ProgressComponent}
        ]
    },
];

@NgModule({
    imports:[RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PagesRoutinModule{}