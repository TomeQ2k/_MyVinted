import { StripeCardElementOptions } from "@stripe/stripe-js";
import { NotifierOptions } from "angular-notifier";

export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api/',
  signalRUrl: 'http://localhost:5000/api/hub/'
};

export const constants = {
  minUsernameLength: 5,
  maxUsernameLength: 24,
  minPasswordLength: 5,
  maxPasswordLength: 30,

  maxTitleLength: 150,
  maxDescriptionLength: 1500,
  maxMessageLength: 300,
  maxPrice: 5000,

  maxPhoneNumberLength: 9,

  maxFilesCount: 5,
  maxFileSize: 3,

  unitConversionMultiplier: 1024,
  moneyMultiplier: 100
};

export const googleCredentials = {
  clientId: 'CLIENT_ID'
};

export const facebookCredentials = {
  appId: 'APP_ID'
};

export const publicStripeApiKey = 'PUBLIC_STRIPE_API_KEY';

export const pageSize = 10;
export const logsPageSize = 50;

export const roles = {
  premium: ['Premium'],
  admin: ['Admin']
};

export const hubNames = {
  notifier: 'notifier',
  messenger: 'messenger'
};

export const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'right',
      distance: 12
    },
    vertical: {
      position: 'bottom',
      distance: 12,
      gap: 10
    }
  },
  theme: 'material',
  behaviour: {
    autoHide: 5000,
    onClick: false,
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 1
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};

export const stripeCardOptions: StripeCardElementOptions = {
  style: {
    base: {
      iconColor: '#666EE8',
      color: '#31325F',
      fontWeight: '300',
      fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
      fontSize: '18px',
      '::placeholder': {
        color: '#CFD7E0'
      }
    }
  }
};