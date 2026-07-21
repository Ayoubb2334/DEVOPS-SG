import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './modules/layout/layout.component';
import { WelcomeComponent } from './modules/welcome/welcome.component';
import { ListComponent } from './modules/list/list.component';
import { AddComponent } from './modules/add/add.component';
import { EditComponent } from './modules/edit/edit.component';
import { StatsComponent } from './modules/stats/stats.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: 'welcome', component: WelcomeComponent },
      { path: 'smartphones', component: ListComponent },
      { path: 'statistiques', component: StatsComponent },
      { path: 'smartphones/add', component: AddComponent },
      { path: 'smartphones/edit/:id', component: EditComponent },
      { path: '**', redirectTo: 'welcome' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }