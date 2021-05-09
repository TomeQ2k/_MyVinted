import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Message } from 'src/app/models/domain/message';
import { AuthService } from 'src/app/services/auth.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-message-card',
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.scss']
})
export class MessageCardComponent implements OnInit {
  @Input() message: Message;

  @Output() messageDeleted = new EventEmitter<string>();

  currentUserId: string;

  constructor(private messenger: Messenger, private authService: AuthService, private notifier: Notifier) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser.id;
  }

  public likeMessage() {
    this.messenger.likeMessage(this.message.id).subscribe(() => {
      this.message.isLiked = !this.message.isLiked;
    }, error => this.notifier.push(error, 'error'));
  }

  public deleteMessage() {
    if (confirm('Are you sure you want to delete this message?')) {
      this.messenger.deleteMessage(this.message.id).subscribe(() => {
        this.messageDeleted.emit(this.message.id);
      }, error => this.notifier.push(error, 'error'));
    }
  }
}
