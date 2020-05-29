import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/User';
import { USER_STATUS } from 'src/app/shared/UserStatus';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  public users$: Observable<User[]>;
  public allUsers: User[] = new Array<User>();

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private sanitizer: DomSanitizer) {
    this.service.getUsers().subscribe(users =>
      users.forEach(user => {
        user.photoUrl = this.RenderChatPictures(user.bytePhoto);
        user.userStatusString = USER_STATUS[user.userStatus];
        this.allUsers.push(user);
      })
    );
  }

  ngOnInit(): void {
  }

  RenderChatPictures(photoUser: any): any {
    const ObjectURL = 'data:image/jpeg;base64,' + photoUser;
    return this.sanitizer.bypassSecurityTrustUrl(ObjectURL);
  }

}
