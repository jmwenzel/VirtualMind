import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PricingComponent } from './pricing/pricing.component';
import { PurchaseComponent } from './purchase/purchase.component';

const routes: Routes = [
  { path: '', redirectTo: '/pricing', pathMatch: 'full' },
  { path: 'pricing', component: PricingComponent },
  { path: 'purchase', component: PurchaseComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
