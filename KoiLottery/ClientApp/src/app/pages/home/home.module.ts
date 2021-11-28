import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';

import { HomeComponent } from './home.component';
import { LotteriesComponent } from '../../components/lotteries/lotteries.component';

@NgModule({
  declarations: [
    HomeComponent,
    LotteriesComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule
  ],
})
export class HomeModule { }
