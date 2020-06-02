import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { CHAT_PRIVACY } from 'src/app/shared/CHAT_PRIVACY';
import { User } from 'src/app/shared/User';
import { USER_STATUS } from 'src/app/shared/USER_STATUS';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-create-chat',
  templateUrl: './create-chat.component.html',
  styleUrls: ['./create-chat.component.scss']
})
export class CreateChatComponent implements OnInit {

  public createChatFormModel: any;
  public privacyArray: Array<any>;
  public dropDownMenuName = 'Choose Chat Privacy';
  public chatPrivacyStatus: any;
  public selectedFile: File = null;
  public selectedFileName = 'Select File';
  public allUsers: User[] = new Array<User>();

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private route: ActivatedRoute,
              private sanitizer: DomSanitizer) {
   }

  ngOnInit(): void {
    this.privacyArray = new Array(
      {name: 'Opened', value: CHAT_PRIVACY.Opened},
      {name: 'Private', value: CHAT_PRIVACY.Private},
    );

    this.service.getUsers().subscribe(users =>
      users.forEach(user => {
        user.photoUrl = this.RenderChatPictures(user.bytePhoto);
        user.userStatusString = USER_STATUS[user.userStatus];
        this.allUsers.push(user);
      })
    );

    this.createChatFormModel = this.fb.group({
      Name: [''],
      Password: ['']
    });
  }

  onFileSelected(event) {
    this.selectedFile = event.target.files[0] as File;
    this.selectedFileName = this.selectedFile.name;
  }

  onSelectPrivacy(privacyStatus: CHAT_PRIVACY) {
    this.chatPrivacyStatus = privacyStatus;
    this.dropDownMenuName = this.privacyArray[privacyStatus].name;
  }

  onCreateChat(): void {
    const formData = new FormData();
    if (!this.selectedFile) {
      formData.append('picture', this.selectedFile, this.selectedFile.name);
    }
    formData.append('name', this.createChatFormModel.value.Name);
    formData.append('chatPrivacy', this.chatPrivacyStatus);
    formData.append('password', this.createChatFormModel.value.Password);
    formData.append('chatUsers', '5ebeba1863e2f91e7028d20c');

    this.service.createChat(formData)
      .subscribe((res: any) => {
        this.router.navigateByUrl('/main');
      });

    this.createChatFormModel.reset();
  }

  RenderChatPictures(photoUser: any): any {
    const ObjectURL = 'data:image/jpeg;base64,' + photoUser;
    return this.sanitizer.bypassSecurityTrustUrl(ObjectURL);
  }
}
