import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatDashboardComponent } from './chat-dashboard/chat-dashboard.component';
import { CreateChatContainerComponent } from './create-chat/create-chat-container.component';
import { ChatProfileContainerComponent } from './chat-profile/chat-profile-container.component';
import { UserProfileContainerComponent } from './user-profile/user-profile-container.component';


const routes: Routes = [
  { path: '', component: ChatDashboardComponent,
    children: [
      { path: 'createchat', component: CreateChatContainerComponent },
      { path: 'chat/:id', component: ChatProfileContainerComponent },
      { path: 'user', component: UserProfileContainerComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChatDashboardRoutingModule { }
