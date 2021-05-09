import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderType } from 'src/app/enums/order-type.enum';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { Listener } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-premium-payment',
  templateUrl: './premium-payment.component.html',
  styleUrls: ['./premium-payment.component.scss']
})
export class PremiumPaymentComponent implements OnInit {
  premiumPrice: number = 9.99;

  constructor(private cartService: CartService, private router: Router, private notifier: Notifier, private authService: AuthService,
    private listener: Listener) { }

  ngOnInit(): void {
    if (this.authService.currentUser.isVerified) {
      this.router.navigate(['']);
    }

    this.listener.changeCurrentNavbarFormVisible(false);
  }

  public addUpgradeAccountToCart() {
    this.cartService.addToCart({
      amount: this.premiumPrice,
      type: OrderType.Premium,
      productName: 'Upgrade account to PREMIUM',
      userName: this.authService.currentUser.userName,
      email: this.authService.currentUser.email
    }).subscribe(() => {
      this.router.navigate(['/cart']);
      this.notifier.push('Upgrade account has been added to your cart');
    }, error => {
      this.notifier.push(error, 'error');
      this.router.navigate(['/cart']);
    });
  }
}
