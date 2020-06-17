import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/User';
import { USER_STATUS } from 'src/app/shared/USER_STATUS';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  @Input() public allUsers: User[];

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private sanitizer: DomSanitizer) {
  }

  ngOnInit(): void {
  }

}
