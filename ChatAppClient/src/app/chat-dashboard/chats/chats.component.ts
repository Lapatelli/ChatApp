import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { ChatService } from 'src/app/services/chat.service';
import { Observable } from 'rxjs';
import { Chat } from 'src/app/shared/Chat';
import { DomSanitizer } from '@angular/platform-browser';
import { CHAT_PRIVACY } from 'src/app/shared/ChatPrivacy';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.scss']
})
export class ChatsComponent implements OnInit {

  public chats$: Observable<Chat[]>;
  public allChats: Chat[] = new Array<Chat>();

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private sanitizer: DomSanitizer) {
    this.service.getChats().subscribe(chats =>
      chats.forEach(chat => {
        chat.pictureUrl = this.RenderChatPictures(chat.picture);
        chat.chatPrivacyString = CHAT_PRIVACY[chat.chatPrivacy];
        this.allChats.push(chat);
      })
    );
  }

  ngOnInit(): void {
  }

  RenderChatPictures(pictureChat: any): any {
    const ObjectURL = 'data:image/jpeg;base64,' + pictureChat;
    return this.sanitizer.bypassSecurityTrustUrl(ObjectURL);
  }

}
