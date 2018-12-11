import { Component, OnInit, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'appc-error-summary',
    styleUrls: ['./error-summary.component.scss'],
    templateUrl: './error-summary.component.html'
})
export class ErrorSummaryComponent implements OnInit, OnDestroy {
    notification: Notification;
    sub: any;
    constructor(    
        private toastr: ToastrService,
        ) {
    }

    mapNotificationType(type: string) {
        if (type === 'error') {
            return 'danger';
        }
        return type;
    }
    ngOnInit() {
        
        // this.sub = this.ns.getChangeEmitter()
        //     .subscribe((x: NotificationEvent) => {
        //         if (x.add) {
        //             this.notification = x.notification;
        //         } else {
        //             this.notification = null;
        //         }
        //     });
    }

    ngOnDestroy() {
        // this.sub.unsubscribe();
    }
}
