import { NgModule } from '@angular/core';
import { ExtraOptions, RouterModule, Routes, PreloadAllModules } from '@angular/router';


const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then(m => m.DashboardModule)
  }
];

const config: ExtraOptions = {
  useHash: true,
  enableTracing: false, // Turn this on to log routing events to the console
  scrollPositionRestoration: 'enabled'
  , preloadingStrategy: PreloadAllModules
};

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
