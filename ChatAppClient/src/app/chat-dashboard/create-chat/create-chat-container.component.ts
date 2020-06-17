import { Component, OnDestroy } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CreateChatComponent } from './create-chat.component';

@Component({
  selector: 'app-modal-container',
  template: ''
})
export class CreateChatContainerComponent implements OnDestroy {
  destroy = new Subject<any>();
  currentDialog = null;

  constructor(private modalService: NgbModal, route: ActivatedRoute, router: Router) {
    route.paramMap.pipe(takeUntil(this.destroy)).subscribe(params => {
      this.currentDialog = this.modalService.open(CreateChatComponent, {centered: true});

      this.currentDialog.result.then(result => {
        router.navigateByUrl('/main');
      },
      reason => {
        router.navigateByUrl('/main');
      });
    });
  }

  ngOnDestroy() {
    this.currentDialog.close();
  }
}
