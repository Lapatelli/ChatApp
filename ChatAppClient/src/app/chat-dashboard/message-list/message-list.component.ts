import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/services/chat.service';
import { FormBuilder } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { SignalRService } from 'src/app/services/signal-r.service';
import { ChatMessage } from 'src/app/shared/Message';
import { Chat } from 'src/app/shared/Chat';
import { UsersComponent } from '../users/users.component';
import { User } from 'src/app/shared/User';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.scss']
})
export class MessageListComponent implements OnInit {

  @Input() public selectedChat: Chat;
  @Input() allUsers: User[];
  @Input() allChats: Chat[];
  @Input() public userEmail: string;

  constructor(private router: Router, private service: ChatService, private fb: FormBuilder, private sanitizer: DomSanitizer,
              private signalRService: SignalRService) { }

  ngOnInit(): void {
    this.signalRService.messagereceived.subscribe((message: ChatMessage) => {
      message.photo = this.allUsers.find(us => us.emailAddress === message.email).photoUrl;
      this.allChats.find(ch => ch.name === message.chat).messageStorage.push(message);
    });
  }

}
