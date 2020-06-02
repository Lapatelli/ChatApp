import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../shared/User';
import { Observable } from 'rxjs';
import { URLs } from '../shared/URLs';
import { SSL_OP_COOKIE_EXCHANGE } from 'constants';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public getUserById(userId: string): Observable<User> {
    return this.httpClient.get<User>(URLs.UserUrl + userId + '/profile',  {withCredentials: true});
  }

  public updateChat(userId: string, file: FormData): Observable<any> {
    return this.httpClient.put<any>(URLs.UserUrl + userId + '/update', file,  {withCredentials: true});
  }
}
