import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { URLs } from '../shared/URLs';
import { Subject } from 'rxjs';
import { Data } from '@angular/router';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { ChatMessage } from '../shared/Message';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;
  public messagereceived = new EventEmitter<ChatMessage>();
  private testSubject$: Subject<string> = new Subject<string>();

  constructor() {
    this.buildConnection();
    this.startConnection();
  }

  public buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(URLs.BaseUrl + 'chat-hub')
      .build();
  }

  public startConnection = () => {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection Started...');
        this.onSendMessage();
        this.onAddToChat();
      })
      .catch(err => {
        console.log('Error while trying to connect: ' + err);

        setTimeout(function() { this.startConnection(); }, 3000);
      });
  }

  public addToChat(chatId: string): void {
      this.hubConnection.send('addToChat', chatId);
  }

  public onAddToChat(): void {
      this.hubConnection.on('OnAddToChat', (data: string) => {
        console.log(data);
      });
  }

  public sendMessage(chatId: string, message: ChatMessage): void{
    this.hubConnection.send('sendMessage', chatId, message);
  }

  public onSendMessage() {
    this.hubConnection.on('onSendMessage', (data: ChatMessage) => {
      this.messagereceived.emit(data);
    });
  }
}
