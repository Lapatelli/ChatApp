import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { USER_STATUS } from 'src/app/shared/UserStatus';
import { CHAT_PRIVACY } from 'src/app/shared/ChatPrivacy';
import { ICreateChat} from 'src/app/shared/Chat-create';

@Component({
  selector: 'app-create-chat',
  templateUrl: './create-chat.component.html',
  styleUrls: ['./create-chat.component.scss']
})
export class CreateChatComponent implements OnInit {

  public createChatFormModel: any;
  public privacyArray: Array<any>;
  public dropDownMenuName: string = 'Choose Chat Privacy';
  public chatPrivacyStatus: any;
  public selectedFile: File = null;
  public selectedFileName: string = 'Select File';
  public createChatModel: ICreateChat = {
    name: '',
    chatPrivacy: CHAT_PRIVACY.Opened,
    password: '',
    picture: null,
    chatUsers: new Array<string>()
  };

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private route: ActivatedRoute) {
   }

  ngOnInit(): void {
    this.privacyArray = new Array(
      {name: 'Opened', value: CHAT_PRIVACY.Opened},
      {name: 'Private', value: CHAT_PRIVACY.Private},
    );

    this.createChatFormModel = this.fb.group({
      Name: [''],
      Password: [''],
      Users: ['']
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
    formData.append('picture', this.selectedFile, this.selectedFile.name);
    formData.append('name', this.createChatFormModel.value.Name);
    formData.append('chatPrivacy', this.chatPrivacyStatus);
    formData.append('password', this.createChatFormModel.value.Password);
    formData.append('chatUsers', '5ebeba1863e2f91e7028d20c');

    console.log(this.createChatModel);

    this.service.createChat(formData)
      .subscribe((res: any) => {
        this.router.navigateByUrl('/main');
      });

    this.createChatFormModel.reset();
  }



}
