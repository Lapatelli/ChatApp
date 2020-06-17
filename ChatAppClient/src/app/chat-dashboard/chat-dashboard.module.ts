import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatDashboardRoutingModule } from './chat-dashboard-routing.module';
import { ChatDashboardComponent } from './chat-dashboard/chat-dashboard.component';
import { ChatsComponent } from './chats/chats.component';
import { ChatService } from '../services/chat.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersComponent } from './users/users.component';
import { CreateChatContainerComponent } from './create-chat/create-chat-container.component';
import { CreateChatComponent } from './create-chat/create-chat.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ChatProfileComponent } from './chat-profile/chat-profile.component';
import { ChatProfileContainerComponent } from './chat-profile/chat-profile-container.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserService } from '../services/user.service';
import { UserProfileContainerComponent } from './user-profile/user-profile-container.component';
import { AuthService } from '../services/auth.service';
import { MessageListComponent } from './message-list/message-list.component';


@NgModule({
  declarations: [
    ChatDashboardComponent,
    ChatsComponent,
    UsersComponent,
    CreateChatContainerComponent,
    ChatProfileContainerComponent,
    UserProfileContainerComponent,
    CreateChatComponent,
    ChatProfileComponent,
    UserProfileComponent,
    MessageListComponent
  ],
  imports: [
    CommonModule,
    ChatDashboardRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [
    ChatService,
    UserService,
    AuthService
  ],
  bootstrap: [
    ChatDashboardComponent
  ]
})
export class ChatDashboardModule { }
