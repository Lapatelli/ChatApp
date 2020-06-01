import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../shared/User';
import { Observable } from 'rxjs';
import { URLs } from '../shared/URLs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public getUserById(userId: string): Observable<User> {
    return this.httpClient.get<User>(URLs.UserUrl + userId + '/profile');
  }

  public updateChat(userId: string, file: FormData): Observable<any> {
    return this.httpClient.put<any>(URLs.UserUrl + userId + '/update', file);
  }
}
