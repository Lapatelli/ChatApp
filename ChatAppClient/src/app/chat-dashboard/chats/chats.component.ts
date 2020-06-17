import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { ChatService } from 'src/app/services/chat.service';
import { Observable } from 'rxjs';
import { Chat } from 'src/app/shared/Chat';
import { DomSanitizer } from '@angular/platform-browser';
import { CHAT_PRIVACY } from 'src/app/shared/CHAT_PRIVACY';
import { SignalRService } from 'src/app/services/signal-r.service';
import { ChatMessage } from 'src/app/shared/Message';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.scss']
})
export class ChatsComponent implements OnInit {

  @Output() selectChat = new EventEmitter<Chat>();
  @Input() allChats: Chat[];

  // public chats$: Observable<Chat[]>;
  // public allChats: Chat[] = new Array<Chat>();

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private sanitizer: DomSanitizer,
              private signalRService: SignalRService) {
    // this.service.getChats().subscribe(chats => {
    //   chats.forEach(chat => {
    //     chat.pictureUrl = this.RenderChatPictures(chat.picture);
    //     chat.chatPrivacyString = CHAT_PRIVACY[chat.chatPrivacy];
    //     chat.messageStorage = new Array<ChatMessage>();
    //     this.allChats.push(chat);
    //   });

    //   this.allChats.forEach((chat: Chat ) => {
    //     this.signalRService.addToChat(chat.id);
    //   });
    // });
  }

  ngOnInit(): void {
  }

  // RenderChatPictures(pictureChat: any): any {
  //   const ObjectURL = 'data:image/jpeg;base64,' + pictureChat;
  //   return this.sanitizer.bypassSecurityTrustUrl(ObjectURL);
  // }

  onSelectChat( chat: Chat ) {
    this.selectChat.emit(chat);
  }
}
