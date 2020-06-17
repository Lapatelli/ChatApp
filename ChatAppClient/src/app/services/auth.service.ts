import { Injectable, Inject } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { URLs } from '../shared/URLs';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(@Inject(DOCUMENT) private document: Document, private httpClient: HttpClient) { }

  public loginGoogle() {
    this.document.location.href = URLs.AccountUrl + 'google-login';
  }

  public logOut() {
    this.document.location.href = URLs.AccountUrl + 'logout';
    // return this.httpClient.get(URLs.AccountUrl + 'logout', {withCredentials: true});
  }
}
