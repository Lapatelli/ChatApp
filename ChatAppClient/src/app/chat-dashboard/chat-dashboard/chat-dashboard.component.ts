import { Component, OnInit } from '@angular/core';
import { Chat } from 'src/app/shared/Chat';
import { SignalRService } from 'src/app/services/signal-r.service';
import { FormBuilder } from '@angular/forms';
import { ChatMessage } from 'src/app/shared/Message';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-chat-dashboard',
  templateUrl: './chat-dashboard.component.html',
  styleUrls: ['./chat-dashboard.component.scss']
})
export class ChatDashboardComponent implements OnInit {

  public selectedChat: Chat = null;
  public listToggler = true;
  public userProfile = '5ebebb2b67f12f5c20faba8e';
  public inputMessage = '';
  public callerMessage =  new ChatMessage();
  public chatMessageStorage: Array<ChatMessage> = new Array<ChatMessage>();

  constructor(private signalRService: SignalRService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.signalRService.messagereceived.subscribe((message: ChatMessage) => {
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
    this.callerMessage.userId = '5ebebb2b67f12f5c20faba8e';

    this.signalRService.sendMessage(this.callerMessage);

    this.inputMessage = '';
  }
}
