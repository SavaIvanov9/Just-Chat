import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AngularMaterialModule } from '../external-modules/angular-material.module';
import { AppButtonComponent } from './app-button/app-button.component';

@NgModule({
  imports: [FormsModule, CommonModule, AngularMaterialModule],
  declarations: [AppButtonComponent],
  providers: [],
  exports: [AppButtonComponent]
})
export class ControlsModule {}
