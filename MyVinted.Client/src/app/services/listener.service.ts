import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Category } from '../models/domain/category';
import { Order } from '../models/domain/order';
import { UserAuth } from '../models/domain/user-auth';
import { OffersRequest } from '../resolvers/requests/offers-request';
import { UsersRequest } from '../resolvers/requests/users-request';

@Injectable({
  providedIn: 'root'
})
export class Listener {
  private user = new BehaviorSubject<UserAuth>(null);
  currentUser = this.user.asObservable();

  private categories = new BehaviorSubject<Category[]>([]);
  currentCategories = this.categories.asObservable();

  private offersRequest = new BehaviorSubject<OffersRequest>(new OffersRequest());
  currentOffersRequest = this.offersRequest.asObservable();

  private usersRequest = new BehaviorSubject<UsersRequest>(new UsersRequest());
  currentUsersRequest = this.usersRequest.asObservable();

  private navbarFormCleared = new BehaviorSubject<boolean>(false);
  currentNavbarFormCleared = this.navbarFormCleared.asObservable();

  private navbarFormVisible = new BehaviorSubject<boolean>(true);
  currentNavbarFormVisible = this.navbarFormVisible.asObservable();

  private filteredOrders = new BehaviorSubject<Order[]>([]);
  currentFilteredOrders = this.filteredOrders.asObservable();

  public changeCurrentUser(user: UserAuth) {
    if (user) {
      localStorage.setItem('user', JSON.stringify(user));
    }

    this.user.next(user);
  }

  public changeCurrentCategories(categories: Category[]) {
    this.categories.next(categories);
  }

  public changeCurrentOffersRequest(offersRequest: OffersRequest) {
    this.offersRequest.next(offersRequest);
  }

  public changeCurrentUsersRequest(usersRequest: UsersRequest) {
    this.usersRequest.next(usersRequest);
  }

  public clearCurrentNavbarForm() {
    this.navbarFormCleared.next(true);
  }

  public changeCurrentNavbarFormVisible(navbarFormVisible: boolean) {
    this.navbarFormVisible.next(navbarFormVisible);
  }

  public changeCurrentFilteredOrders(orders: Order[]) {
    this.filteredOrders.next(orders);
  }
}
