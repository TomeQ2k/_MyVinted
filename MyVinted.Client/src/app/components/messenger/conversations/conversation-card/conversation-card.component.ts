import { Component, Input, OnInit } from '@angular/core';
import { Conversation } from 'src/app/models/helpers/conversation';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-conversation-card',
  templateUrl: './conversation-card.component.html',
  styleUrls: ['./conversation-card.component.scss']
})
export class ConversationCardComponent implements OnInit {
  @Input() conversation: Conversation;

  currentUserId: string;
  currentUserAvatarUrl: string;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser.id;
    this.currentUserAvatarUrl = this.authService.currentUser.avatarUrl;
  }
}
