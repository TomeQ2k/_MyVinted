import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Filterable } from 'src/app/helpers/interfaces/filterable';
import { Category } from 'src/app/models/domain/category';
import { OffersRequest } from 'src/app/resolvers/requests/offers-request';
import { AuthService } from 'src/app/services/auth.service';
import { Listener } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { hubNames, roles } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, Filterable {
  filterForm: FormGroup;

  categories: Category[] = [];

  private offersRequest: OffersRequest;

  isSignedIn: boolean;
  searchFormVisible = true;

  unreadNotificationsCount: number;
  unreadMessagesCount: number;

  userName: string;
  avatarUrl: string;
  isVerified: boolean;

  roles = roles;

  constructor(private authService: AuthService, private router: Router, private notifier: Notifier, private listener: Listener,
    private formBuilder: FormBuilder, private signalr: Signalr, private notificationService: NotificationService, private messenger: Messenger) { }

  ngOnInit(): void {
    this.createFilterForm();
    this.subscribeData();
  }

  public emitFilters() {
    this.offersRequest = Object.assign(this.offersRequest, this.filterForm.value);
    this.listener.changeCurrentOffersRequest(this.offersRequest);
  }

  public clearFilters() { }

  public logout() {
    this.authService.signOut();
    this.router.navigate(['/login']);
    this.notifier.push('Signed out');

    this.signalr.closeConnection(hubNames.notifier);
    this.signalr.closeConnection(hubNames.messenger);
  }

  private subscribeData() {
    this.authService.currentSignedIn.subscribe(signedIn => this.isSignedIn = signedIn);
    this.listener.currentUser.subscribe(user => {
      this.userName = user?.userName;
      this.avatarUrl = user?.avatarUrl;
      this.isVerified = user?.isVerified;
    });

    this.notificationService.currentUnreadNotificationsCount.subscribe(count => this.unreadNotificationsCount = count);
    this.messenger.currentUnreadMessagesCount.subscribe(count => this.unreadMessagesCount = count);

    this.listener.currentOffersRequest.subscribe(request => this.offersRequest = request);
    this.listener.currentCategories.subscribe(categories => this.categories = categories);
    this.listener.currentNavbarFormCleared.subscribe(() => this.createFilterForm());
    this.listener.currentNavbarFormVisible.subscribe(isVisible => this.searchFormVisible = isVisible);
  }

  private createFilterForm() {
    this.filterForm = this.formBuilder.group({
      title: [''],
      categoryId: [null]
    });
  }
}
