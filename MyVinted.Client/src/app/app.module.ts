import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import localeEn from '@angular/common/locales/en';
import localeEnExtra from '@angular/common/locales/extra/en';
import { SocialLoginModule } from 'angularx-social-login';
import { ExternalAuthServiceProvider } from './services/client-external-auth.service';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { registerLocaleData } from '@angular/common';
import { RouterModule } from '@angular/router';
import { routes } from './routes';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { NotifierModule } from 'angular-notifier';
import { customNotifierOptions, publicStripeApiKey } from 'src/environments/environment';
import { InterceptorProvider } from './services/interceptor/interceptor.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/identity/login/login.component';
import { RegisterComponent } from './components/identity/register/register.component';
import { ResetPasswordComponent } from './components/identity/reset-password/reset-password.component';
import { ConfirmAccountComponent } from './components/identity/register/confirm-account/confirm-account.component';
import { ResetPasswordConfirmComponent } from './components/identity/reset-password/reset-password-confirm/reset-password-confirm.component';
import { ChangeEmailComponent } from './components/account/change-email/change-email.component';
import { ChangeUsernameComponent } from './components/account/change-username/change-username.component';
import { ChangePhoneComponent } from './components/account/change-phone/change-phone.component';
import { AvatarUploaderComponent } from './components/account/avatar-uploader/avatar-uploader.component';
import { ChangePasswordComponent } from './components/account/change-password/change-password.component';
import { AccountComponent } from './components/account/account.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { NotificationCardComponent } from './components/notifications/notification-card/notification-card.component';
import { OfferCardComponent } from './components/offers/offer-card/offer-card.component';
import { OffersFilterFormComponent } from './components/offers/offers-filter-form/offers-filter-form.component';
import { FavoritesComponent } from './components/favorites/favorites.component';
import { NewOfferComponent } from './components/offers/new-offer/new-offer.component';
import { AddOfferButtonComponent } from './components/helpers/add-offer-button/add-offer-button.component';
import { MyOffersComponent } from './components/my-offers/my-offers.component';
import { EditOfferComponent } from './components/offers/edit-offer/edit-offer.component';
import { CartComponent } from './components/cart/cart.component';
import { CheckoutSummaryComponent } from './components/cart/checkout-summary/checkout-summary.component';
import { OfferDetailsComponent } from './components/offers/offer-details/offer-details.component';
import { OpinionsComponent } from './components/offers/offer-details/opinions/opinions.component';
import { OpinionFormComponent } from './components/offers/offer-details/opinions/opinion-form/opinion-form.component';
import { PhotosGalleryComponent } from './components/helpers/photos-gallery/photos-gallery.component';
import { PaginationComponent } from './components/helpers/pagination/pagination.component';
import { OpinionCardComponent } from './components/offers/offer-details/opinions/opinion-card/opinion-card.component';
import { UserComponent } from './components/user/user.component';
import { UserCardComponent } from './components/user/user-card/user-card.component';
import { UploadPhotoButtonComponent } from './components/helpers/upload-photo-button/upload-photo-button.component';
import { ValidationErrorComponent } from './components/helpers/validation-error/validation-error.component';
import { ChangeEmailConfirmComponent } from './components/account/change-email/change-email-confirm/change-email-confirm.component';
import { ProfileResolver } from './resolvers/profile.resolver';
import { CategoriesResolver } from './resolvers/categories.resolver';
import { NumericDirective } from './directives/numeric.directive';
import { HostListenerComponent } from './components/helpers/host-listener/host-listener.component';
import { OfferUpdateResolver } from './resolvers/offer-update.resolver';
import { OffersResolver } from './resolvers/offers.resolver';
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import { OfferResolver } from './resolvers/offer.resolver';
import { FavoritesOffersResolver } from './resolvers/favorites-offers.resolver';
import { EmptyListInfoComponent } from './components/helpers/empty-list-info/empty-list-info.component';
import { MyOffersResolver } from './resolvers/my-offers.resolver';
import { UsersResolver } from './resolvers/users.resolver';
import { UsersComponent } from './components/users/users.component';
import { UserResolver } from './resolvers/user.resolver';
import { UsersFiltersFormComponent } from './components/users/users-filters-form/users-filters-form.component';
import { NotificationsResolver } from './resolvers/notifications.resolver';
import { NumberBadgeComponent } from './components/helpers/number-badge/number-badge.component';
import { LettersLimiterComponent } from './components/helpers/letters-limiter/letters-limiter.component';
import { AuctionFormComponent } from './components/offers/offer-details/auction-form/auction-form.component';
import { ConversationsComponent } from './components/messenger/conversations/conversations.component';
import { MessagesThreadComponent } from './components/messenger/messages-thread/messages-thread.component';
import { InfiniteScrollComponent } from './components/helpers/infinite-scroll/infinite-scroll.component';
import { ConversationsResolver } from './resolvers/conversations.resolver';
import { MessagesResolver } from './resolvers/messages.resolver';
import { ConversationCardComponent } from './components/messenger/conversations/conversation-card/conversation-card.component';
import { MessageCardComponent } from './components/messenger/messages-thread/message-card/message-card.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgxStripeModule } from 'ngx-stripe';
import { PaymentComponent } from './components/payment/payment.component';
import { CartResolver } from './resolvers/cart.resolver';
import { OrdersResolver } from './resolvers/orders.resolver';
import { OrdersComponent } from './components/orders/orders.component';
import { PremiumPaymentComponent } from './components/account/premium-payment/premium-payment.component';
import { PremiumStatsResolver } from './resolvers/premium-stats.resolver';
import { AdminStatsResolver } from './resolvers/admin-stats.resolver';
import { RequiredRolesDirective } from './directives/required-roles.directive';
import { PremiumStatsComponent } from './components/premium-stats/premium-stats.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { AdminStatsComponent } from './components/admin-panel/admin-stats/admin-stats.component';
import { StatsListComponent } from './components/helpers/stats-list/stats-list.component';
import { MessageFormComponent } from './components/helpers/message-form/message-form.component';
import { LogsComponent } from './components/admin-panel/logs-panel/logs.component';
import { LogsResolver } from './resolvers/logs.resolver';
import { AllOrdersResolver } from './resolvers/all-orders.resolver';
import { AdminOrdersComponent } from './components/orders/admin-orders/admin-orders.component';
import { UserOrdersComponent } from './components/orders/user-orders/user-orders.component';

export const tokenGetter = () => localStorage.getItem('token');

registerLocaleData(localeEn, 'en', localeEnExtra);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    ResetPasswordComponent,
    ConfirmAccountComponent,
    ResetPasswordConfirmComponent,
    ChangeEmailComponent,
    ChangeUsernameComponent,
    ChangePhoneComponent,
    AvatarUploaderComponent,
    ChangePasswordComponent,
    AccountComponent,
    NotificationsComponent,
    NotificationCardComponent,
    OfferCardComponent,
    OffersFilterFormComponent,
    FavoritesComponent,
    NewOfferComponent,
    AddOfferButtonComponent,
    MyOffersComponent,
    EditOfferComponent,
    CartComponent,
    CheckoutSummaryComponent,
    OfferDetailsComponent,
    OpinionsComponent,
    OpinionFormComponent,
    PhotosGalleryComponent,
    PaginationComponent,
    OpinionCardComponent,
    UserComponent,
    UserCardComponent,
    UsersComponent,
    UploadPhotoButtonComponent,
    ValidationErrorComponent,
    ChangeEmailConfirmComponent,
    NumericDirective,
    HostListenerComponent,
    EmptyListInfoComponent,
    UsersFiltersFormComponent,
    NumberBadgeComponent,
    LettersLimiterComponent,
    AuctionFormComponent,
    ConversationsComponent,
    MessagesThreadComponent,
    InfiniteScrollComponent,
    ConversationCardComponent,
    MessageCardComponent,
    PaymentComponent,
    OrdersComponent,
    PremiumPaymentComponent,
    TimeAgoPipe,
    RequiredRolesDirective,
    PremiumStatsComponent,
    AdminPanelComponent,
    AdminStatsComponent,
    StatsListComponent,
    MessageFormComponent,
    LogsComponent,
    AdminOrdersComponent,
    UserOrdersComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,

    InfiniteScrollModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),

    NotifierModule.withConfig(customNotifierOptions),
    FontAwesomeModule,
    SocialLoginModule,

    NgxStripeModule.forRoot(publicStripeApiKey)
  ],
  providers: [
    ProfileResolver,
    CategoriesResolver,
    OfferResolver,
    OffersResolver,
    OfferUpdateResolver,
    FavoritesOffersResolver,
    MyOffersResolver,
    UsersResolver,
    UserResolver,
    NotificationsResolver,
    ConversationsResolver,
    MessagesResolver,
    CartResolver,
    OrdersResolver,
    AllOrdersResolver,
    PremiumStatsResolver,
    AdminStatsResolver,
    LogsResolver,

    InterceptorProvider,
    ExternalAuthServiceProvider,
    { provide: LOCALE_ID, useValue: 'en-EN' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
