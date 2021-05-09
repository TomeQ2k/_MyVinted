import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderType } from 'src/app/enums/order-type.enum';
import { Validatable } from 'src/app/helpers/interfaces/validatable';
import { Offer } from 'src/app/models/domain/offer';
import { ValidationErrorTuple } from 'src/app/models/helpers/validation-error-tuple';
import { AuctionService } from 'src/app/services/auction.service';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-auction-form',
  templateUrl: './auction-form.component.html',
  styleUrls: ['./auction-form.component.scss']
})
export class AuctionFormComponent implements OnInit, Validatable {
  @Input() offer: Offer;

  auctionForm: FormGroup;

  currentUserId: string;

  constructor(private authService: AuthService, private auctionService: AuctionService, private formBuilder: FormBuilder, private notifier: Notifier,
    private cartService: CartService, private router: Router, private listener: Listener) { }

  ngOnInit(): void {
    this.createAuctionForm();
    this.currentUserId = this.authService.currentUser.id;
  }

  public addToCart() {
    this.cartService.addToCart({
      amount: this.offer.price,
      type: OrderType.Offer,
      productName: this.offer.title,
      userName: this.authService.currentUser.userName,
      email: this.authService.currentUser.email,
      offerId: this.offer.id,
      offerOwnerId: this.offer.ownerId
    }).subscribe(() => {
      this.router.navigate(['/cart']);
      this.notifier.push('Item has been added to your cart');
    }, error => this.notifier.push(error, 'error'));
  }

  public proposePrice() {
    if (this.auctionForm.valid) {
      const request = Object.assign({}, this.auctionForm.value);

      this.auctionService.createOfferAuction(request).subscribe(res => {
        const response: any = res.body;
        this.notifier.push(`Price ${request.newPrice} $ was proposed`);
        this.offer.offerAuction = response.offerAuction;
      }, error => this.notifier.push(error, 'error'),
        () => this.createAuctionForm());
    }
  }

  public acceptAuction() {
    this.auctionService.acceptOfferAuction(this.offer.offerAuction.id, this.offer.id).subscribe(() => {
      this.notifier.push('Auction was accepted and new price has been set');
      this.offer.offerAuction.isAccepted = true;
      this.offer.price = this.offer.offerAuction.newPrice;
    }, error => this.notifier.push(error, 'error'));
  }

  public denyAuction() {
    this.auctionService.denyOfferAuction(this.offer.offerAuction.id, this.offer.id).subscribe(() => {
      this.notifier.push('Auction was denied and price has been not changed');
      this.offer.offerAuction = null;
    }, error => this.notifier.push(error, 'error'));
  }

  public getValidationErrorTuple = (control: string): ValidationErrorTuple => new ValidationErrorTuple(control, this.auctionForm);

  private createAuctionForm() {
    this.auctionForm = this.formBuilder.group({
      newPrice: [(this.offer.price - 0.01).toFixed(2), [Validators.required, Validators.min(1), Validators.max(this.offer.price - 0.01)]],
      offerId: [this.offer.id, [Validators.required]]
    });
  }
}
