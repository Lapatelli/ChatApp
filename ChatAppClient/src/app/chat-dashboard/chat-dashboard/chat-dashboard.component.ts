import { Component, OnInit, ViewChild } from '@angular/core';
import { Chat } from 'src/app/shared/Chat';
import { SignalRService } from 'src/app/services/signal-r.service';
import { FormBuilder } from '@angular/forms';
import { ChatMessage } from 'src/app/shared/Message';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/shared/User';
import { UsersComponent } from '../users/users.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-chat-dashboard',
  templateUrl: './chat-dashboard.component.html',
  styleUrls: ['./chat-dashboard.component.scss']
})
export class ChatDashboardComponent implements OnInit {

  @ViewChild(UsersComponent, {static: false})
  private usersComponent: UsersComponent;

  public selectedChat: Chat = null;
  public listToggler = true;
  public userEmail = '';
  public user = new User();
  public inputMessage = '';
  public callerMessage =  new ChatMessage();
  public chatMessageStorage: Array<ChatMessage> = new Array<ChatMessage>();

  constructor(private signalRService: SignalRService, private fb: FormBuilder, private userService: UserService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe((email: string) => {
      this.userEmail = email;
    });

    this.signalRService.messagereceived.subscribe((message: ChatMessage) => {
      message.photo = this.usersComponent.allUsers.find(us => us.emailAddress === message.email).photoUrl;
      this.chatMessageStorage.unshift(message);
    });
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

    this.signalRService.sendMessage(this.callerMessage);

    this.inputMessage = '';
  }

  onLogOut() {
    this.authService.logOut();
  }
}
