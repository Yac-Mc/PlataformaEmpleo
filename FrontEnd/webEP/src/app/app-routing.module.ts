import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Modulos
import { PagesRoutinModule } from './pages/pages.routing';
import { AuthRoutinModule } from './auth/auth.routing';

import { NoPageFoundComponent } from './no-page-found/no-page-found.component';

const routes: Routes = [
  {path: '', redirectTo:'login', pathMatch:'full'},
  {path: '**', component: NoPageFoundComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    PagesRoutinModule,
    AuthRoutinModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
