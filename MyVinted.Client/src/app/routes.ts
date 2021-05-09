import { Routes } from "@angular/router";
import { roles } from "src/environments/environment";
import { AccountComponent } from "./components/account/account.component";
import { ChangeEmailConfirmComponent } from "./components/account/change-email/change-email-confirm/change-email-confirm.component";
import { PremiumPaymentComponent } from "./components/account/premium-payment/premium-payment.component";
import { AdminPanelComponent } from "./components/admin-panel/admin-panel.component";
import { AdminStatsComponent } from "./components/admin-panel/admin-stats/admin-stats.component";
import { LogsComponent } from "./components/admin-panel/logs-panel/logs.component";
import { CartComponent } from "./components/cart/cart.component";
import { CheckoutSummaryComponent } from "./components/cart/checkout-summary/checkout-summary.component";
import { FavoritesComponent } from "./components/favorites/favorites.component";
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/identity/login/login.component";
import { ConfirmAccountComponent } from "./components/identity/register/confirm-account/confirm-account.component";
import { RegisterComponent } from "./components/identity/register/register.component";
import { ResetPasswordConfirmComponent } from "./components/identity/reset-password/reset-password-confirm/reset-password-confirm.component";
import { ResetPasswordComponent } from "./components/identity/reset-password/reset-password.component";
import { ConversationsComponent } from "./components/messenger/conversations/conversations.component";
import { MessagesThreadComponent } from "./components/messenger/messages-thread/messages-thread.component";
import { MyOffersComponent } from "./components/my-offers/my-offers.component";
import { NotificationsComponent } from "./components/notifications/notifications.component";
import { EditOfferComponent } from "./components/offers/edit-offer/edit-offer.component";
import { NewOfferComponent } from "./components/offers/new-offer/new-offer.component";
import { OfferDetailsComponent } from "./components/offers/offer-details/offer-details.component";
import { AdminOrdersComponent } from "./components/orders/admin-orders/admin-orders.component";
import { UserOrdersComponent } from "./components/orders/user-orders/user-orders.component";
import { PremiumStatsComponent } from "./components/premium-stats/premium-stats.component";
import { UserComponent } from "./components/user/user.component";
import { UsersComponent } from "./components/users/users.component";
import { AnonymousGuard } from "./guards/anonymous.guard";
import { AuthGuard } from "./guards/auth.guard";
import { AdminStatsResolver } from "./resolvers/admin-stats.resolver";
import { OrdersResolver } from "./resolvers/orders.resolver";
import { CartResolver } from "./resolvers/cart.resolver";
import { CategoriesResolver } from "./resolvers/categories.resolver";
import { ConversationsResolver } from "./resolvers/conversations.resolver";
import { FavoritesOffersResolver } from "./resolvers/favorites-offers.resolver";
import { LogsResolver } from "./resolvers/logs.resolver";
import { MessagesResolver } from "./resolvers/messages.resolver";
import { MyOffersResolver } from "./resolvers/my-offers.resolver";
import { NotificationsResolver } from "./resolvers/notifications.resolver";
import { OfferUpdateResolver } from "./resolvers/offer-update.resolver";
import { OfferResolver } from "./resolvers/offer.resolver";
import { OffersResolver } from "./resolvers/offers.resolver";
import { UserOrdersResolver } from "./resolvers/user-orders.resolver";
import { PremiumStatsResolver } from "./resolvers/premium-stats.resolver";
import { ProfileResolver } from "./resolvers/profile.resolver";
import { UserResolver } from "./resolvers/user.resolver";
import { UsersResolver } from "./resolvers/users.resolver";

export const routes: Routes = [
  {
    path: '', component: HomeComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard],
    resolve: { offersResponse: OffersResolver, categoriesResponse: CategoriesResolver }
  },
  { path: 'checkout/summary/:orderId', component: CheckoutSummaryComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'favorites', component: FavoritesComponent, resolve: { favoritesResponse: FavoritesOffersResolver, categoriesResponse: CategoriesResolver } },
      { path: 'myOffers', component: MyOffersComponent, resolve: { myOffersResponse: MyOffersResolver, categoriesResponse: CategoriesResolver } },
      { path: 'notifications', component: NotificationsComponent, resolve: { notificationsResponse: NotificationsResolver } },
      { path: 'cart', component: CartComponent, resolve: { cartResponse: CartResolver } },
      { path: 'account', component: AccountComponent, resolve: { profileResponse: ProfileResolver } },
      { path: 'account/changeEmail/confirm', component: ChangeEmailConfirmComponent },
      { path: 'offers/:offerId', component: OfferDetailsComponent, resolve: { offerResponse: OfferResolver } },
      { path: 'newOffer', component: NewOfferComponent, resolve: { categoriesResponse: CategoriesResolver } },
      { path: 'editOffer/:offerId', component: EditOfferComponent, resolve: { offerUpdateResponse: OfferUpdateResolver, categoriesResponse: CategoriesResolver } },
      { path: 'users', component: UsersComponent, resolve: { usersResponse: UsersResolver } },
      { path: 'users/:userId', component: UserComponent, resolve: { userResponse: UserResolver } },
      { path: 'messenger', component: ConversationsComponent, resolve: { conversationsResponse: ConversationsResolver } },
      { path: 'messenger/:recipientId', component: MessagesThreadComponent, resolve: { messagesResponse: MessagesResolver } },
      { path: 'orders', component: UserOrdersComponent, resolve: { ordersResponse: UserOrdersResolver } },
      { path: 'account/upgrade', component: PremiumPaymentComponent },
      { path: 'stats', component: PremiumStatsComponent, resolve: { statsResponse: PremiumStatsResolver }, data: { roles: roles.premium } },
      { path: 'admin', component: AdminPanelComponent, data: { roles: roles.admin } },
      { path: 'admin/stats', component: AdminStatsComponent, resolve: { statsResponse: AdminStatsResolver }, data: { roles: roles.admin } },
      { path: 'admin/logs', component: LogsComponent, resolve: { logsResponse: LogsResolver }, data: { roles: roles.admin } },
      { path: 'admin/orders', component: AdminOrdersComponent, resolve: { ordersResponse: OrdersResolver }, data: { roles: roles.admin } }
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AnonymousGuard],
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'resetPassword', component: ResetPasswordComponent },
      { path: 'register/confirm', component: ConfirmAccountComponent },
      { path: 'resetPassword/confirm', component: ResetPasswordConfirmComponent },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
