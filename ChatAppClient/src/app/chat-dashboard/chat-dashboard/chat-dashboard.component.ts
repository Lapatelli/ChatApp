import { Component, OnInit } from '@angular/core';
import { Chat } from 'src/app/shared/Chat';

@Component({
  selector: 'app-chat-dashboard',
  templateUrl: './chat-dashboard.component.html',
  styleUrls: ['./chat-dashboard.component.scss']
})
export class ChatDashboardComponent implements OnInit {

  public selectedChat: Chat = null;
  public listToggler = true;
  public userProfile = '5ebebb2b67f12f5c20faba8e';

  constructor() { }

  ngOnInit(): void {
  }

  onToggleList(panel: boolean) {
    this.listToggler = panel;
  }

  selectChat(chat: Chat) {
    this.selectedChat = chat;
  }
}
