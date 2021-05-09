import { AfterViewChecked, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'src/app/models/domain/message';
import { Recipient } from 'src/app/models/domain/recipient';
import { Pagination } from 'src/app/models/helpers/pagination';
import { MessengerRequest } from 'src/app/resolvers/requests/messenger-request';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { hubNames } from 'src/environments/environment';

@Component({
  selector: 'app-messages-thread',
  templateUrl: './messages-thread.component.html',
  styleUrls: ['./messages-thread.component.scss']
})
export class MessagesThreadComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('chat') chatSection: ElementRef;

  messages: Message[];
  pagination: Pagination;
  recipient: Recipient;

  isScrolled = false;
  private isMessengerOpened = true;

  private messengerRequest = new MessengerRequest();

  constructor(private messenger: Messenger, private route: ActivatedRoute, private notifier: Notifier,
    private signalr: Signalr, private authService: AuthService, private listener: Listener) { }

  ngOnInit(): void {
    this.subscribeData();

    this.listener.changeCurrentNavbarFormVisible(false);

    this.subscribeSignalr();
  }

  ngOnDestroy(): void {
    this.isMessengerOpened = false;
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  public onMessageSent(message: any) {
    this.isScrolled = false;
    this.messages.push(message);
    this.scrollToBottom();
  }

  public onScroll() {
    if (this.messages.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.getMessages(true);
      this.isScrolled = true;
    }
  }

  public onDeleteMessage(messageId: string) {
    this.messages = this.messages.filter(m => m.id !== messageId);
  }

  private getMessages(onScroll = false) {
    if (this.isMessengerOpened) {
      this.messengerRequest.pageNumber = onScroll ? this.pagination.currentPage : 1;

      this.messenger.getMessagesThread(this.recipient.id, this.messengerRequest).subscribe(response => {
        const messages = response.result.messages;

        this.messages.reverse();
        this.messages = onScroll ? this.messages.concat(messages) : messages;
        this.messages.reverse();

        this.pagination = response?.pagination;
      }, error => this.notifier.push(error, 'error'));
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.messages = data.messagesResponse.result.messages;
      this.pagination = data.messagesResponse.pagination;
      this.recipient = data.messagesResponse.result.recipient;

      this.messenger.decrementCurrentUnreadMessagesCount();

      this.messages.reverse();
    });
  }

  private subscribeSignalr() {
    this.signalr.subscribeAction(SIGNALR_ACTIONS.MESSAGE_RECEIVED, hubNames.messenger, values => {
      if (this.authService.isSignedIn() && values[0].senderId === this.recipient.id && this.isMessengerOpened) {
        this.messages.push(values[0]);

        this.messenger.readMessage(values[0].id).subscribe(() => {
          const messageIndex = this.messages.findIndex(m => m.id === values[0].id);
          this.messages[messageIndex] = { ...this.messages[messageIndex], isRead: true };
          this.messenger.decrementCurrentUnreadMessagesCount();
        });

        this.scrollToBottom();
      }
    });
  }

  private scrollToBottom() {
    if (this.chatSection && !this.isScrolled) {
      this.chatSection.nativeElement.scrollTop = this.chatSection.nativeElement.scrollHeight;
    }
  }
}
