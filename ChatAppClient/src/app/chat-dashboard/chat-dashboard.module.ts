import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatDashboardRoutingModule } from './chat-dashboard-routing.module';
import { ChatDashboardComponent } from './chat-dashboard/chat-dashboard.component';
import { ChatsComponent } from './chats/chats.component';
import { ChatService } from '../services/chat.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersComponent } from './users/users.component';
import { ModalContainerComponent } from './modal-container.component';
import { CreateChatComponent } from './create-chat/create-chat.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    ChatDashboardComponent,
    ChatsComponent,
    UsersComponent,
    ModalContainerComponent,
    CreateChatComponent
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
    ChatService
  ],
  bootstrap: [
    ChatDashboardComponent
  ]
})
export class ChatDashboardModule { }
