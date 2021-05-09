import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { StripeElementLocale, StripeElementsOptions } from '@stripe/stripe-js';
import { StripeCardComponent } from 'ngx-stripe';
import { stripeCardOptions } from 'src/environments/environment';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit {
  @ViewChild(StripeCardComponent) card: StripeCardComponent;

  @Input() locale: StripeElementLocale = 'en';

  @Output() stripeCard = new EventEmitter<StripeCardComponent>();

  paymentForm: FormGroup;

  cardOptions = stripeCardOptions;

  elementsOptions: StripeElementsOptions = {
    locale: this.locale
  };

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.paymentForm = this.formBuilder.group({});
    this.stripeCard.emit(this.card);
  }
}
