import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';

import { CreateAccountComponent } from './create.component';

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild([
            { path: '', component: CreateAccountComponent}
        ])
    ],
    declarations: [CreateAccountComponent]
})
export class CreateAccountModule { }
