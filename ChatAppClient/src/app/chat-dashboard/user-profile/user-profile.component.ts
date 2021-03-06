import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { CHAT_PRIVACY } from 'src/app/shared/CHAT_PRIVACY';
import { ChatWithUsers } from 'src/app/shared/ChatWithUsers';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/shared/User';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  public user = new User();
  public userFormModel = this.fb.group({
    FirstName: [''],
    LastName: [''],
  });
  public selectedFile: File = null;
  public selectedFileName = 'Select File';

  constructor(private router: Router, private service: UserService, private fb: FormBuilder, private route: ActivatedRoute) {
   }

  ngOnInit(): void {
    this.service.getUserById()
      .subscribe(((result: User) => {

        this.user = result;
        this.onChatFormInitilalize(this.user);
      }));
  }

  onChatFormInitilalize(userProfile: User) {
    this.userFormModel = this.fb.group({
      FirstName: [userProfile.firstName],
      LastName: [userProfile.lastName],
    });
  }

  onFileSelected(event) {
    this.selectedFile = event.target.files[0] as File;
    this.selectedFileName = this.selectedFile.name;
  }

  onUpdateUser(): void {
    const formData = new FormData();
    if (this.selectedFile !== null) {
      formData.append('photo', this.selectedFile, this.selectedFile.name);
    }
    formData.append('firstname', this.userFormModel.value.FirstName);
    formData.append('lastname', this.userFormModel.value.LastName);

    this.service.updateChat(formData)
      .subscribe((res: any) => {
        this.router.navigateByUrl('/main');
      });

    this.userFormModel.reset();
  }
}
