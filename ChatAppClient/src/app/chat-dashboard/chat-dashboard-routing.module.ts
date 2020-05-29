import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatDashboardComponent } from './chat-dashboard/chat-dashboard.component';
import { ModalContainerComponent } from './modal-container.component';
import { CreateChatComponent } from './create-chat/create-chat.component';


const routes: Routes = [
  { path: '', component: ChatDashboardComponent,
    children: [
      { path: 'createchat', component: ModalContainerComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChatDashboardRoutingModule { }
