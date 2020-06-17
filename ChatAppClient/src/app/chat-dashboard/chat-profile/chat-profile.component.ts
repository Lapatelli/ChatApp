import { Component, OnInit, Input } from '@angular/core';
import { ICreateChat } from 'src/app/shared/ICreateChat';
import { CHAT_PRIVACY } from 'src/app/shared/CHAT_PRIVACY';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatWithUsers } from 'src/app/shared/ChatWithUsers';
import { User } from 'src/app/shared/User';
import { DomSanitizer } from '@angular/platform-browser';
import { USER_STATUS } from 'src/app/shared/USER_STATUS';

@Component({
  selector: 'app-chat-profile',
  templateUrl: './chat-profile.component.html',
  styleUrls: ['./chat-profile.component.scss']
})
export class ChatProfileComponent implements OnInit {

  @Input()
  public chatId: string;

  public chatWithUsers = new ChatWithUsers();
  public privacyArray: Array<any>;
  public dropDownMenuName = 'Choose Chat Privacy';
  public chatPrivacyStatus: any;
  public selectedFile: File = null;
  public selectedFileName = 'Select File';

  public editChatFormModel = this.fb.group({
    Name: [''],
    ChatPrivacy: [''],
    Password: ['']
  });

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private route: ActivatedRoute,
              private sanitizer: DomSanitizer) {
   }

  ngOnInit(): void {
    this.privacyArray = new Array(
      {name: 'Opened', value: CHAT_PRIVACY.Opened},
      {name: 'Private', value: CHAT_PRIVACY.Private},
    );

    this.service.getChatById(this.chatId)
      .subscribe(((result: ChatWithUsers) => {
        this.onSelectPrivacy(result.chatPrivacy);

        this.chatWithUsers = result;
        this.chatWithUsers.chatUsers.forEach((user: User) => {
          user.photoUrl = this.RenderChatPictures(user.bytePhoto);
          user.userStatusString = USER_STATUS[user.userStatus];
        });
        this.onChatFormInitilalize(this.chatWithUsers);
      }));
  }

  onChatFormInitilalize(chat: ChatWithUsers) {
    this.editChatFormModel = this.fb.group({
      Name: [chat.name],
      Password: [chat.password],
      Users: [chat.chatUsers]
    });
  }

  RenderChatPictures(photoUser: any): any {
    const ObjectURL = 'data:image/jpeg;base64,' + photoUser;
    return this.sanitizer.bypassSecurityTrustUrl(ObjectURL);
  }

  onFileSelected(event) {
    this.selectedFile = event.target.files[0] as File;
    this.selectedFileName = this.selectedFile.name;
  }

  onSelectPrivacy(privacyStatus: CHAT_PRIVACY) {
    this.chatPrivacyStatus = privacyStatus;
    this.dropDownMenuName = this.privacyArray[privacyStatus].name;
  }

  onUpdateChat(): void {
    const formData = new FormData();
    console.log(this.editChatFormModel.value.Password);
    if (this.selectedFile !== null) {
      formData.append('picture', this.selectedFile, this.selectedFile.name);
    }
    formData.append('name', this.editChatFormModel.value.Name);
    formData.append('chatPrivacy', this.chatPrivacyStatus);
    formData.append('password', this.editChatFormModel.value.Password);

    this.service.updateChat(this.chatWithUsers.id, formData)
      .subscribe((res: any) => {
        this.router.navigateByUrl('/main');
      });

    this.editChatFormModel.reset();
  }
}
