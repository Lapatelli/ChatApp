import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Chat } from '../shared/Chat';
import { URLs } from '../shared/URLs';
import { User } from '../shared/User';
import { ICreateChat } from '../shared/Chat-create';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private httpClient: HttpClient) { }

  public getChats(): Observable<Chat[]> {
    return this.httpClient.get<Chat[]>(URLs.UserUrl + '5ebeba1863e2f91e7028d20c' + '/allchats');
  }

  public getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(URLs.UserUrl + '5ebeba1863e2f91e7028d20c' + '/allusers');
  }

  // public createChat(createdChat: ICreateChat): Observable<Chat> {
  //   return this.httpClient.post<Chat>(URLs.ChatUrl + 'create?userId=5ebebb2b67f12f5c20faba8e', createdChat);
  // }

  public createChat(file: FormData): Observable<any> {
    console.log(file);
    return this.httpClient.post<any>(URLs.ChatUrl + 'create?userId=5ebebb2b67f12f5c20faba8e', file);
  }
}
