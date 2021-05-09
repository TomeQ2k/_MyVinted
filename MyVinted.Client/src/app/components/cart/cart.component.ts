import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StripeCardComponent } from 'ngx-stripe';
import { OrderType } from 'src/app/enums/order-type.enum';
import { arrayReducer } from 'src/app/helpers/array-reducer';
import { Cart } from 'src/app/models/domain/cart';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { Notifier } from 'src/app/services/notifier.service';
import { OrderService } from 'src/app/services/order.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cart: Cart;

  stripeCard: StripeCardComponent;

  moneyMultiplier = constants.moneyMultiplier;

  paymentFormVisible = false;

  constructor(private orderService: OrderService, private cartService: CartService, private notifier: Notifier, private route: ActivatedRoute,
    private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.cart = data.cartResponse.cart);
  }

  public purchaseOrder() {
    this.orderService.createToken(this.stripeCard.element, { name: this.authService.currentUser.email }).subscribe(response => {
      if (response.token) {
        const token = response.token;

        if (token) {
          this.orderService.purchaseOrder({ tokenId: token.id, totalAmount: this.cart.totalAmount, email: this.authService.currentUser.email }).subscribe(res => {
            const response: any = res.body;
            if (response.isSucceeded) {
              const isUpgradeAccount = response.order.items.some(i => i.type === OrderType.Premium);

              if (isUpgradeAccount) {
                this.notifier.push('Account has been upgraded. In order to apply these changes, you have to sign in again', 'success');
              } else {
                this.notifier.push('Order has been completed', 'success');
              }

              this.router.navigate(['/checkout/summary/', response.order.id, { isUpgradeAccount }]);
            } else {
              this.notifier.push('Error occurred during purchasing order', 'error');
            }
          }, error => this.notifier.push(error, 'error'))
        } else {
          this.notifier.push('Error occurred during purchasing order', 'error');
        }
      } else if (response.error) {
        console.error(response.error.message);
        this.notifier.push(response.error.message, 'error');
      }
    });
  }

  public removeFromCart(itemId: string) {
    this.cartService.removeFromCart(itemId).subscribe(() => {
      this.notifier.push('Item has been removed from your cart');
      this.cart.items = this.cart.items.filter(i => i.id !== itemId);
      if (this.cart.items.length > 0) {
        const itemsAmounts: number[] = this.cart.items.map(i => i.amount);
        this.cart.totalAmount = itemsAmounts.reduce(arrayReducer);
      } else {
        this.cart.totalAmount = 0;
      }
    }, error => this.notifier.push(error, 'error'));
  }

  public clearCart() {
    if (confirm('Are you sure you want to clear your cart?')) {
      this.cartService.clearCart().subscribe(() => {
        this.notifier.push('Cart has been cleared');
        this.cart = null;
      }, error => this.notifier.push(error, 'error'));
    }
  }

  public togglePaymentFormVisible = () => this.paymentFormVisible = !this.paymentFormVisible;

  public getItemType = (type: OrderType) => {
    switch (type) {
      case OrderType.Offer: return 'OFFER';
      case OrderType.Premium: return 'PREMIUM'
      default: return 'OFFER';
    }
  };
}
