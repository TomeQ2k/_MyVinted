import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Conversation } from 'src/app/models/helpers/conversation';
import { Pagination } from 'src/app/models/helpers/pagination';
import { MessengerRequest } from 'src/app/resolvers/requests/messenger-request';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { hubNames } from 'src/environments/environment';

@Component({
  selector: 'app-conversations',
  templateUrl: './conversations.component.html',
  styleUrls: ['./conversations.component.scss']
})
export class ConversationsComponent implements OnInit {
  conversations: Conversation[];
  pagination: Pagination;

  filtersForm: FormGroup;

  private messengerRequest = new MessengerRequest();

  constructor(private messenger: Messenger, private route: ActivatedRoute, private signalr: Signalr, private authService: AuthService,
    private notifier: Notifier, private formBuilder: FormBuilder, private listener: Listener) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.conversations = data.conversationsResponse.result.conversations;
      this.pagination = data.conversationsResponse.pagination;
    });

    this.createFiltersForm();

    this.listener.changeCurrentNavbarFormVisible(false);

    this.signalr.subscribeAction(SIGNALR_ACTIONS.MESSAGE_RECEIVED, hubNames.messenger, () => {
      if (this.authService.isSignedIn()) {
        this.getConversations();
        this.messenger.countUnreadMessages();
      }
    });
  }

  public onFiltersChanged() {
    this.messengerRequest = { ...this.messengerRequest, pageNumber: 1, userName: this.filtersForm.value.userName }
    this.getConversations();
  }

  public onScroll() {
    if (this.conversations.length < this.pagination.totalItems) {
      this.messengerRequest.pageNumber++;
      this.getConversations(true);
    }
  }

  private getConversations(onScroll = false) {
    this.messenger.getConversations(this.messengerRequest).subscribe(response => {
      const conversations = response.result.conversations;
      this.conversations = onScroll ? [...this.conversations, ...conversations] : conversations;
      this.pagination = response.pagination;
    }, error => this.notifier.push(error, 'error'));
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      userName: ['']
    });
  }
}
