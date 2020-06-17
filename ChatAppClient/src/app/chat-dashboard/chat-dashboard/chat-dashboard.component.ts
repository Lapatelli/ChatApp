import { Component, OnInit, ViewChild } from '@angular/core';
import { Chat } from 'src/app/shared/Chat';
import { SignalRService } from 'src/app/services/signal-r.service';
import { FormBuilder } from '@angular/forms';
import { ChatMessage } from 'src/app/shared/Message';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/shared/User';
import { UsersComponent } from '../users/users.component';
import { AuthService } from 'src/app/services/auth.service';
import { ChatsComponent } from '../chats/chats.component';
import { ChatService } from 'src/app/services/chat.service';
import { USER_STATUS } from 'src/app/shared/USER_STATUS';
import { Observable } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';
import { CHAT_PRIVACY } from 'src/app/shared/CHAT_PRIVACY';

@Component({
  selector: 'app-chat-dashboard',
  templateUrl: './chat-dashboard.component.html',
  styleUrls: ['./chat-dashboard.component.scss']
})
export class ChatDashboardComponent implements OnInit {

  @ViewChild(UsersComponent, {static: false})
  public usersComponent: UsersComponent;

  public selectedChat: Chat = null;
  public listToggler = true;
  public userEmail = '';
  public user = new User();
  public inputMessage = '';
  public callerMessage =  new ChatMessage();
  public allUsers: User[] = new Array<User>();
  public allChats: Chat[] = new Array<Chat>();

  constructor(private signalRService: SignalRService, private fb: FormBuilder, private userService: UserService,
              private sanitizer: DomSanitizer,
              private chatService: ChatService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe((email: string) => {
      this.userEmail = email;
    });

    this.chatService.getUsers().subscribe(users =>
      users.forEach(user => {
        user.photoUrl = this.RenderChatPictures(user.bytePhoto);
        user.userStatusString = USER_STATUS[user.userStatus];
        this.allUsers.push(user);
      })
    );

    this.chatService.getChats().subscribe(chats => {
      chats.forEach(chat => {
        chat.pictureUrl = this.RenderChatPictures(chat.picture);
        chat.chatPrivacyString = CHAT_PRIVACY[chat.chatPrivacy];
        chat.messageStorage = new Array<ChatMessage>();
        this.allChats.push(chat);
      });

      this.allChats.forEach((chat: Chat ) => {
        this.signalRService.addToChat(chat.id);
      });
    });
  }

  RenderChatPictures(photoUser: any): any {
    const ObjectURL = 'data:image/jpeg;base64,' + photoUser;
    return this.sanitizer.bypassSecurityTrustUrl(ObjectURL);
  }

  onToggleList(panel: boolean) {
    this.listToggler = panel;
  }

  selectChat(chat: Chat) {
    this.selectedChat = chat;
  }

  onSendMessage(message: string) {

    this.callerMessage.content = message;
    this.callerMessage.time = new Date().toLocaleString();
    this.callerMessage.email = this.userEmail;
    this.callerMessage.chat = this.selectedChat.name;

    this.signalRService.sendMessage(this.selectedChat.id, this.callerMessage);

    this.inputMessage = '';
  }

  onLogOut() {
    this.authService.logOut();
  }
}
