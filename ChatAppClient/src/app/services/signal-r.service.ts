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
      })
      .catch(err => {
        console.log('Error while trying to connect: ' + err);

        setTimeout(function() { this.startConnection(); }, 3000);
      });
  }

  testSubject() {
    return this.testSubject$.asObservable();
  }

  public sendMessage(message: ChatMessage): void{
    this.hubConnection.send('sendMessage', message);

  }
  public onSendMessage() {
    this.hubConnection.on('OnSendMessage', (data: ChatMessage) => {
      this.messagereceived.emit(data);
    });
  }
}
